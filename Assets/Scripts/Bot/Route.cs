using Assets.Scripts.Bot;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] List<RoutePoint> routeObjectPoints;
    public List<Vector2> RoutePoints;
    private List<RouteDistanceBetweenPoints> pointsDistances = new();

    private void Awake()
    {
        RoutePoints = routeObjectPoints.OrderBy(p => p.Order).Select(p => (Vector2)p.transform.position).ToList();
        for (int i = 1; i < RoutePoints.Count; i++)
        {
            var distance = new RouteDistanceBetweenPoints(i - 1, i, RoutePoints);
            pointsDistances.Add(distance);
        }
        var firstLastPointDistance = new RouteDistanceBetweenPoints(0, RoutePoints.Count - 1, RoutePoints);
        pointsDistances.Add(firstLastPointDistance);
    }

    public float GetDistanceBetweenRouteClosestPoints(int point1Index, int point2Index)
    {
        return pointsDistances.FirstOrDefault(d =>
        (d.Point1Index == point1Index || d.Point1Index == point2Index) && (d.Point2Index == point2Index || d.Point2Index == point1Index))?.Distance ?? 0;
    }


    private void OnDrawGizmosSelected()
    {
        if (!(RoutePoints?.Any() ?? false))
        {
            return;
        }
        Gizmos.color = Color.yellow;
        for (int i = 1; i < RoutePoints.Count; i++)
        {
            Gizmos.DrawLine(RoutePoints[i - 1], RoutePoints[i]);
        }
        Gizmos.DrawLine(RoutePoints.First(), RoutePoints.Last());
    }
}
