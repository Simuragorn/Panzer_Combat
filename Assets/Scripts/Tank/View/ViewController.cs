using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ViewController : MonoBehaviour
{
    public delegate void OnEnemyTargetChanged(Tank tank);
    public event OnEnemyTargetChanged onEnemyTargetChanged;

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
            onEnemyTargetChanged?.Invoke(enemyTarget);
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
            onEnemyTargetChanged?.Invoke(enemyTarget);
        }
    }

    protected bool TrySetNewTarget(Tank newTarget)
    {
        return enemyTarget == null && newTarget.IsHuman != isHuman;
    }
}
