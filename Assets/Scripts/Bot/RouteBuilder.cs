using Assets.Scripts.Bot;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RouteBuilder : MonoBehaviour
{
    public UnityEvent<Vector2> OnNextPointChanged = new();
    public UnityEvent OnTargetPointReached = new();
    protected Tank tank;
    [SerializeField] protected Route route;
    protected List<Vector2> routePoints;
    protected int nextPointIndex;

    void Start()
    {
        tank = GetComponentInChildren<Tank>();
        routePoints = route.RoutePoints;
        SetFirstPoint();
    }

    protected void SetFirstPoint()
    {
        nextPointIndex = GetClosestPoint(tank.transform.position);
        OnNextPointChanged?.Invoke(routePoints[nextPointIndex]);
    }

    public void PointReached(Vector2 point)
    {
        if (point == routePoints[nextPointIndex])
        {
            OnTargetPointReached?.Invoke();
        }
    }

    public void SetNextPoint(Vector2? target = null)
    {
        if (target == null)
        {
            nextPointIndex = GetNextRouteIndex();
        }
        else
        {
            nextPointIndex = GetNextRouteIndexForTarget(target.Value);
        }
        OnNextPointChanged?.Invoke(routePoints[nextPointIndex]);
    }

    protected (float forwardDistance, float backwardDistance) GetPointsDistanceByRoute(int point1Index, int point2Index)
    {
        float forwardDistance = 0;
        float backwardDistance = 0;

        int currentIndex = point1Index;
        int end = point2Index;
        while (currentIndex != end)
        {
            int previousIndex = currentIndex;
            currentIndex++;
            if (currentIndex >= routePoints.Count)
            {
                currentIndex = 0;
            }
            forwardDistance += route.GetDistanceBetweenRouteClosestPoints(previousIndex, currentIndex);
        }

        currentIndex = point2Index;
        end = point1Index;
        while (currentIndex != end)
        {
            int previousIndex = currentIndex;
            currentIndex++;
            if (currentIndex >= routePoints.Count)
            {
                currentIndex = 0;
            }
            backwardDistance += route.GetDistanceBetweenRouteClosestPoints(previousIndex, currentIndex);
        }

        return (forwardDistance, backwardDistance);
    }

    protected int GetNextRouteIndex()
    {
        if (nextPointIndex == routePoints.Count - 1)
        {
            return 0;
        }
        return nextPointIndex + 1;
    }

    protected int GetNextRouteIndexForTarget(Vector2 target)
    {
        int closestPointIndex = GetClosestPoint(target);
        if (closestPointIndex == nextPointIndex)
        {
            return nextPointIndex;
        }
        var (forwardDistance, backwardDistance) = GetPointsDistanceByRoute(nextPointIndex, closestPointIndex);
        int resultIndex = nextPointIndex +
            (forwardDistance < backwardDistance ? 1 : -1);

        if (resultIndex >= routePoints.Count)
        {
            return 0;
        }
        if (resultIndex < 0)
        {
            return routePoints.Count - 1;
        }

        return resultIndex;
    }

    protected int GetClosestPoint(Vector2 target)
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

    private void OnDrawGizmos()
    {
        if (routePoints?.Any() ?? false)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(routePoints[nextPointIndex], 1);
        }
    }
}
