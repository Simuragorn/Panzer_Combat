using Assets.Scripts.Bot;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] List<RoutePoint> routeObjectPoints;
    private List<Vector2> _routePoints;
    private List<RouteDistanceBetweenPoints> pointsDistances = new();

    public List<Vector2> RoutePoints
    {
        get
        {
            if (_routePoints == null)
            {
                InitRoutePoints();
            }
            return _routePoints;
        }
        private set
        {
            _routePoints = value;
        }
    }

    public float GetDistanceBetweenRouteClosestPoints(int point1Index, int point2Index)
    {
        return pointsDistances.FirstOrDefault(d =>
        (d.Point1Index == point1Index || d.Point1Index == point2Index) && (d.Point2Index == point2Index || d.Point2Index == point1Index))?.Distance ?? 0;
    }

    private void InitRoutePoints()
    {
        _routePoints = routeObjectPoints.OrderBy(p => p.Order).Select(p => (Vector2)p.transform.position).ToList();
        for (int i = 1; i < _routePoints.Count; i++)
        {
            var distance = new RouteDistanceBetweenPoints(i - 1, i, _routePoints);
            pointsDistances.Add(distance);
        }
        var firstLastPointDistance = new RouteDistanceBetweenPoints(0, _routePoints.Count - 1, _routePoints);
        pointsDistances.Add(firstLastPointDistance);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 1; i < RoutePoints.Count; i++)
        {
            Gizmos.DrawLine(RoutePoints[i - 1], RoutePoints[i]);
        }
        Gizmos.DrawLine(RoutePoints.First(), RoutePoints.Last());
    }
}
