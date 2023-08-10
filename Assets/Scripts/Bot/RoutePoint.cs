using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoutePoint : MonoBehaviour
{
    public int Order => order;

    [SerializeField] private int order;
    private Vector2 pointPosition;

    private void Start()
    {
        pointPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pointReacher = collision.gameObject.GetComponent<PointReacher>();
        if (pointReacher == null)
        {
            return;
        }
        Tank tank = pointReacher.transform.parent.GetComponent<Tank>();
        if (tank != null && !tank.IsHuman)
        {
            var botRouteBuilder = tank.GetComponentInParent<RouteBuilder>();
            botRouteBuilder.PointReached(pointPosition);
        }
    }
}
