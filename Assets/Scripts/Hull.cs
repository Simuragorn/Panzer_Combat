using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Hull : MonoBehaviour, ITarget
{
    private List<Vector2> polygonPoints;
    private void Start()
    {
        polygonPoints = GetPolygonColliderPoints(PolygonCollider);
    }
    public PolygonCollider2D PolygonCollider;
    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        var target = targetCollider.gameObject.GetComponent<Shell>();
        if (target != null)
        {
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            targetCollider.GetContacts(contacts);
            Vector2 contactPoint = contacts[0].point;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!(polygonPoints?.Any() ?? false))
        {
            return;
        }
        List<Color> colors = new List<Color>
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.gray,
            Color.white,
            Color.black
        };
        //for (int i = 1; i < polygonPoints.Count; i++)
        //{
        //    Gizmos.color = colors[i - 1];
        //    Gizmos.DrawLine(polygonPoints[i - 1], polygonPoints[i]);
        //}
        //Gizmos.color = colors[polygonPoints.Count - 1];
        //Gizmos.DrawLine(polygonPoints[polygonPoints.Count - 1], polygonPoints[0]);
    }

    private List<Vector2> GetPolygonColliderPoints(PolygonCollider2D polygonCollider)
    {
        var polygonPoints = new List<Vector2>();
        if (polygonCollider == null)
        {
            return polygonPoints;
        }
        int pathCount = polygonCollider.pathCount;

        for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
        {
            Vector2[] pathPoints = polygonCollider.GetPath(pathIndex);

            int pointCount = pathPoints.Length;

            for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
            {
                Vector2 point = polygonCollider.transform.TransformPoint(pathPoints[pointIndex]);
                polygonPoints.Add(point);
            }
        }
        return polygonPoints;
    }
}
