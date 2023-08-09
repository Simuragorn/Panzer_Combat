using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class ViewController : MonoBehaviour
{
    public UnityEvent<Tank> OnEnemyTargetChanged = new();

    protected Tank enemyTarget;
    protected CircleCollider2D viewCollider;
    protected bool isHuman;

    public void Init(bool isHuman)
    {
        this.isHuman = isHuman;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var tank = collision.GetComponent<Tank>();
        if (tank == null)
        {
            return;
        }
        if (TrySetNewTarget(tank))
        {
            enemyTarget = tank;
            OnEnemyTargetChanged?.Invoke(enemyTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var tank = collision.GetComponent<Tank>();
        if (tank == null)
        {
            return;
        }
        if (enemyTarget == tank)
        {
            enemyTarget = null;
            OnEnemyTargetChanged?.Invoke(enemyTarget);
        }
    }

    protected bool TrySetNewTarget(Tank newTarget)
    {
        return enemyTarget == null && newTarget.IsHuman != isHuman;
    }
}
