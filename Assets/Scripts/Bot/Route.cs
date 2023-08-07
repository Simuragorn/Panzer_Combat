using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] List<RoutePoint> routeObjectPoints;
    private List<Vector2> _routePoints;
    public List<Vector2> RoutePoints
    {
        get
        {
            if (_routePoints == null)
            {
                _routePoints = routeObjectPoints.OrderBy(p => p.Order).Select(p => (Vector2)p.transform.position).ToList();
            }
            return _routePoints;
        }
        private set
        {
            _routePoints = value;
        }
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
