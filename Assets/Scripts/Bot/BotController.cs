using UnityEngine;

public class BotController : PlayerController
{
    protected ViewController viewController;
    protected RouteBuilder routeBuilder;
    protected Vector2 targetPoint;
    protected Tank enemy;

    protected readonly float MIN_MOVEMENT_DISTANCE = 0.1f;

    protected override void Start()
    {
        base.Start();
        viewController = GetComponentInChildren<ViewController>();
        routeBuilder = GetComponentInChildren<RouteBuilder>();
        viewController.OnEnemyTargetChanged.AddListener(OnEnemyChanged);
        routeBuilder.OnNextPointChanged.AddListener(OnTargetPointChanged);
        routeBuilder.OnTargetPointReached.AddListener(OnTargetPointReached);
    }

    public void Init()
    {
        tank.Init(false);
    }

    protected override void Shooting()
    {
        //tank.Shoot();
    }

    protected override void Movement()
    {
        if (targetPoint == null)
        {
            return;
        }
        if (MIN_MOVEMENT_DISTANCE < Vector2.Distance(targetPoint, tank.transform.position))
        {
            Vector2 vectorToTarget = targetPoint - (Vector2)tank.transform.position;
            float angleWithRight = Vector2.Angle(tank.transform.right, vectorToTarget);
            float angleWithUp = Vector2.Angle(tank.transform.up, vectorToTarget);
            float horizontalAxis = angleWithRight > 90 ? -1 : 1;
            if (Mathf.Abs(angleWithUp) < 1)
            {
                horizontalAxis = 0;
            }
            tank.Rotate(horizontalAxis);
            if (horizontalAxis == 0)
            {
                tank.Move(1);
            }
        }
    }

    protected void OnTargetPointChanged(Vector2 target)
    {
        targetPoint = target;
    }

    protected void OnTargetPointReached()
    {
        if (enemy == null)
        {
            routeBuilder.SetNextPoint();
        }
    }

    protected void OnEnemyChanged(Tank enemy)
    {
        this.enemy = enemy;
        Vector2? enemyPosition = enemy?.transform?.position;
        routeBuilder.SetNextPoint(enemyPosition);
    }
}
