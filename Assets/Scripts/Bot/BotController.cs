using Assets.Scripts.Helpers;
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

    protected bool StaysOnPoint => MIN_MOVEMENT_DISTANCE > Vector2.Distance(targetPoint, tank.transform.position);

    protected override void Movement()
    {
        if (targetPoint == null)
        {
            return;
        }
        float horizontalAxis = 0;
        if (!StaysOnPoint)
        {
            horizontalAxis = VectorHelper.GetRotationAxisToTarget(targetPoint, tank.transform);

            if (horizontalAxis == 0)
            {
                tank.Move(1);
            }
        }
        else if (enemy != null)
        {
            horizontalAxis = VectorHelper.GetRotationAxisToTarget((Vector2)enemy.transform.position, tank.transform);
        }
        tank.Rotate(horizontalAxis);
    }

    protected void OnTargetPointChanged(Vector2 target)
    {
        targetPoint = target;
    }

    protected void OnTargetPointReached()
    {
        Vector2? target = enemy == null ? null : enemy.transform.position;
        routeBuilder.SetNextPoint(target);
    }

    protected void OnEnemyChanged(Tank enemy)
    {
        this.enemy = enemy;
        if (enemy == null && StaysOnPoint)
        {
            routeBuilder.SetNextPoint();
        }
    }
}
