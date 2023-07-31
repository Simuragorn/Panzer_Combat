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
    protected Vector2 inSideNormalVector;


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
        RaycastHit2D[] hits = Physics2D.RaycastAll(collider.transform.position, transform.up);
        var neededHit = hits.FirstOrDefault(h => h.collider.gameObject == target.gameObject);
        if (neededHit.collider == null)
        {
            return;
        }
        contactPoint = neededHit.point;

        if (firstHit && target.gameObject == ownerTank.gameObject)
        {
            firstHit = false;
            return;
        }
        firstHit = false;
        currentTarget = target;
    }

    protected void OnHitTarget(Target target)
    {
        Vector2 shellPosition = transform.position;
        var colliderPoints = target.GetColliderPoints();
        Vector2? crossingVector = VectorHelper.FindVectorThatCrossesTargetPoint(colliderPoints, contactPoint);
        if (!crossingVector.HasValue)
        {
            Debug.Log("Didn't crossed");
            return;
        }
        obstacleVector = crossingVector.Value;
        Vector2 shootingVector = shellPosition - contactPoint;

        inSideNormalVector = target.Collider.GetNormalTowardsCenter(contactPoint, obstacleVector);
        float angle = Vector2.Angle(obstacleVector, shootingVector);
        angle = 90 - angle;
        positionPoint = shellPosition;

        ManageTargetHit(target, angle);
    }

    protected void ManageTargetHit(Target target, float hitAngle)
    {
        float effectiveArmor = target.GetEffectiveArmorThickness(hitAngle);
        Debug.Log($"Real Armor: {target.GetArmorThickness()}. EffectiveArmor {effectiveArmor}");
        ShellHitResultEnum hitResult = target.HitAndGetResult(this, hitAngle);
        float penetrationDividerLoss = 0;

        switch (hitResult)
        {
            case ShellHitResultEnum.Penetrated:
                penetrationDividerLoss = effectiveArmor / actualPenetration;
                break;
            case ShellHitResultEnum.Ricochet:
                penetrationDividerLoss = actualPenetration / effectiveArmor;
                float angleAfterRotation = 2 * hitAngle;
                transform.rotation = Quaternion.Euler(0, 0, angleAfterRotation);
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

        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(positionPoint, 0.7f);

        //Gizmos.color = Color.magenta;
        //Gizmos.DrawLine(obstacleVector, obstacleVector * 0.5f);

        //Gizmos.color = Color.white;
        //Gizmos.DrawLine(inSideNormalVector, inSideNormalVector * 0.5f);
    }
}
