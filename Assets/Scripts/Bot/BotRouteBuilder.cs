using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotRouteBuilder : MonoBehaviour
{
    public delegate void OnNextPointChanged(Vector2 nextPoint);
    public event OnNextPointChanged onNextPointChanged;

    [SerializeField] private Route route;
    private List<Vector2> routePoints;
    private int nextPointIndex;
    private Vector2? target;

    void Start()
    {
        routePoints = route.RoutePoints;
        SetTarget(transform.position);
        target = null;
    }

    public void SetTarget(Vector2 newTarget)
    {
        target = newTarget;
        SetNextPoint();
    }

    public void PointReached(Vector2 point)
    {
        if (point == routePoints[nextPointIndex])
        {
            SetNextPoint();
        }
    }

    private void SetNextPoint()
    {
        if (target == null)
        {
            nextPointIndex = GetNextRouteIndex();
        }
        else
        {
            nextPointIndex = GetClosestPoint(target.Value);
        }
        onNextPointChanged?.Invoke(routePoints[nextPointIndex]);
    }

    private int GetNextRouteIndex()
    {
        if (nextPointIndex == routePoints.Count - 1)
        {
            return 0;
        }
        return nextPointIndex + 1;
    }

    private int GetClosestPoint(Vector2 target)
    {
        float closestDistance = Vector2.Distance(target, routePoints.First());
        int closestPointIndex = 0;
        for (int i = 0; i < routePoints.Count; i++)
        {
            Vector2 point = routePoints[i];
            float distance = Vector2.Distance(target, point);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPointIndex = i;
            }
        }
        return closestPointIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 1; i < routePoints.Count; i++)
        {
            Gizmos.DrawLine(routePoints[i - 1], routePoints[i]);
        }
        Gizmos.DrawLine(routePoints.First(), routePoints.Last());
    }
}
