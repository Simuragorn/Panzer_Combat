using Assets.Scripts;
using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseShell : MonoBehaviour, IDestroyable
{
    public float OriginalSpeed;
    public float OriginalPenetration;

    protected float maxColliderDistance;

    protected float actualSpeed;
    protected float actualPenetration;

    public Collider2D Collider;
    public GameObject VFX;

    protected Vector2 direction;
    protected Vector2 contactPoint;
    protected Vector2 positionPoint;
    protected Vector2 obstacleVector;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected bool IsSpeedAndPenetrationUnderLimit()
    {
        return actualPenetration / OriginalPenetration < ShootingConstants.MinOriginalPenetrationPartForShooting ||
            actualSpeed / OriginalSpeed < ShootingConstants.MinOriginalSpeedPartForShooting;
    }

    public void Launch()
    {
        actualPenetration = OriginalPenetration;
        actualSpeed = OriginalSpeed;
        direction = transform.up;
        maxColliderDistance = Vector2.Distance(Collider.bounds.min, Collider.bounds.max);
        Debug.Log(direction);
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(actualSpeed * Time.fixedDeltaTime * direction);
            yield return new WaitForFixedUpdate();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<BaseTarget>();
        if (target == null)
        {
            return;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);
        RaycastHit2D? hit = hits?.FirstOrDefault(h => h.collider.gameObject == collision.gameObject);
        if (hit == null ||
            hit.Value.collider?.gameObject != collision.gameObject)
        {
            return;
        }
        contactPoint = hit.Value.point;
        OnHitTarget(target);
    }

    protected void OnHitTarget(BaseTarget target)
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
        positionPoint = shellPosition;

        ManageTargetHit(target, angle);
    }

    protected void ManageTargetHit(BaseTarget target, float hitAngle)
    {
        float effectiveArmor = target.GetEffectiveArmorThickness(hitAngle);

        ShellHitResultEnum hitResult = CalculateHitResult(target, hitAngle);
        float penetrationDividerLoss = 0;

        switch (hitResult)
        {
            case ShellHitResultEnum.Penetrated:
                penetrationDividerLoss = effectiveArmor / actualPenetration;
                target.OnPenetrate(this);
                break;
            case ShellHitResultEnum.Ricochet:
                penetrationDividerLoss = actualPenetration / effectiveArmor;
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.z += hitAngle;
                transform.rotation = Quaternion.Euler(currentRotation);
                Debug.Log(direction);
                Debug.Log("Ricochet");
                break;
            case ShellHitResultEnum.ShellDestroyed:
                Destroy();
                break;
        }
        actualPenetration -= actualPenetration * penetrationDividerLoss;
        actualSpeed -= actualSpeed * penetrationDividerLoss / 3;

        if (IsSpeedAndPenetrationUnderLimit())
        {
            Destroy();
        }
    }

    protected ShellHitResultEnum CalculateHitResult(BaseTarget target, float hitAngle)
    {
        float effectiveArmor = target.GetEffectiveArmorThickness(hitAngle);
        float hitAdvantage = actualPenetration / effectiveArmor;
        if (hitAdvantage >= ShootingConstants.MinHitAdvantageForPenetration)
        {
            return ShellHitResultEnum.Penetrated;
        }
        if (hitAdvantage >= ShootingConstants.MinHitAdvantageForRicochet)
        {
            return ShellHitResultEnum.Ricochet;
        }
        return ShellHitResultEnum.ShellDestroyed;
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
