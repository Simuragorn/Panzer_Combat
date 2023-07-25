using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shell : MonoBehaviour
{
    [SerializeField] protected float originalSpeed = 10;
    [SerializeField] protected float originalPenetration = 40;

    protected float maxColliderDistance;

    protected float actualSpeed;
    protected float actualPenetration;

    protected new Collider2D collider;

    protected Tank ownerTank;
    protected bool firstHit = true;

    protected Vector2 shootingDirection;
    protected Vector2 contactPoint;
    protected Vector2 positionPoint;
    protected Vector2 obstacleVector;

    protected Target currentTarget;

    protected void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Launch(Tank tank)
    {
        ownerTank = tank;

        actualPenetration = originalPenetration;
        actualSpeed = originalSpeed;
        shootingDirection = Vector2.up;
        maxColliderDistance = Vector2.Distance(collider.bounds.min, collider.bounds.max);
        StartCoroutine(Move());
    }

    public float GetActualPenetration()
    {
        return actualPenetration;
    }

    protected IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(actualSpeed * Time.fixedDeltaTime * Vector2.up);
            yield return new WaitForFixedUpdate();
        }
    }
    protected void Destroy()
    {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        TrySetTarget(collision);
        if (currentTarget != null)
        {
            OnHitTarget(currentTarget);
            currentTarget = null;
        }
    }

    protected void TrySetTarget(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.001f, 0.001f), 0, transform.up);
        RaycastHit2D? hit = hits?.FirstOrDefault(h => h.collider.gameObject == collision.gameObject);
        if (hit?.collider?.gameObject != collision.gameObject)
        {
            return;
        }

        if (firstHit && target.gameObject == ownerTank.gameObject)
        {
            firstHit = false;
            return;
        }
        firstHit = false;
        contactPoint = hit.Value.point;
        currentTarget = target;
    }

    protected void OnHitTarget(Target target)
    {
        Vector2 shellPosition = transform.position;
        var colliderPoints = target.GetColliderPoints();
        Vector2? crossingVector = VectorHelper.FindVectorThatCrossesTargetPoint(colliderPoints, contactPoint);
        if (!crossingVector.HasValue)
        {
            return;
        }
        obstacleVector = crossingVector.Value;
        Vector2 shootingVector = contactPoint - shellPosition;
        float angle = Vector2.Angle(obstacleVector, shootingVector);
        if (angle > 90)
        {
            angle = 180 - angle;
        }
        Debug.Log($"Angle: {angle}");

        positionPoint = shellPosition;

        ManageTargetHit(target, angle);
    }

    protected void ManageTargetHit(Target target, float hitAngle)
    {
        float effectiveArmor = target.GetEffectiveArmorThickness(hitAngle);

        ShellHitResultEnum hitResult = target.HitAndGetResult(this, hitAngle);
        float penetrationDividerLoss = 0;

        switch (hitResult)
        {
            case ShellHitResultEnum.Penetrated:
                penetrationDividerLoss = effectiveArmor / actualPenetration;
                break;
            case ShellHitResultEnum.Ricochet:
                penetrationDividerLoss = actualPenetration / effectiveArmor;
                Vector3 currentRotation = transform.rotation.eulerAngles;
                float rotationAngle = 2 * hitAngle;
                currentRotation.z -= rotationAngle;
                transform.rotation = Quaternion.Euler(currentRotation);
                Debug.Log("Ricochet");
                break;
            case ShellHitResultEnum.ShellDestroyed:
                Debug.Log("Shell Destroyed");
                Destroy();
                break;
        }
        actualPenetration -= actualPenetration * penetrationDividerLoss;
        actualSpeed -= actualSpeed * penetrationDividerLoss / 3;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(contactPoint, 0.7f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(positionPoint, 0.7f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(obstacleVector, obstacleVector * 0.5f);
    }
}
