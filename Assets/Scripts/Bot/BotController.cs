using UnityEngine;

public class BotController : PlayerController
{
    protected ViewController viewController;
    protected Tank enemyTarget;

    protected override void Start()
    {
        base.Start();
        viewController = GetComponentInChildren<ViewController>();
        viewController.onEnemyTargetChanged += OnTargetChanged;
    }

    public void Init()
    {
        tank.Init(false);
    }

    protected override void Shooting()
    {
        tank.Shoot();
    }

    protected override void Movement()
    {
        if (enemyTarget == null)
        {
            return;
        }
        Vector2 vectorToTarget = enemyTarget.transform.position - tank.transform.position;
        float angleWithRight = Vector2.Angle(tank.transform.right, vectorToTarget);
        float angleWithUp = Vector2.Angle(tank.transform.up, vectorToTarget);
        float horizontalAxis = angleWithRight > 90 ? -1 : 1;
        if (Mathf.Abs(angleWithUp) < 1)
        {
            horizontalAxis = 0;
        }
        tank.Rotate(horizontalAxis);
    }

    protected void OnTargetChanged(Tank target)
    {
        this.enemyTarget = target;
    }
}
