using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class ColliderExtension
    {
        public static List<Vector2> GetColliderPoints(this Collider2D collider)
        {
            var colliderPoints = new List<Vector2>();

            if (collider is PolygonCollider2D)
            {
                PolygonCollider2D polygonCollider = collider as PolygonCollider2D;
                
                if (polygonCollider == null)
                {
                    return colliderPoints;
                }
                int pathCount = polygonCollider.pathCount;

                for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
                {
                    Vector2[] pathPoints = polygonCollider.GetPath(pathIndex);

                    int pointCount = pathPoints.Length;

                    for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
                    {
                        Vector2 point = polygonCollider.transform.TransformPoint(pathPoints[pointIndex]);
                        colliderPoints.Add(point);
                    }
                }
            }
            
            return colliderPoints;
        }
    }
}
