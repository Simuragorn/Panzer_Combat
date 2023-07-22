using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class ColliderExtension
    {
        public static List<Vector2> GetColliderPoints(this Collider2D collider)
        {
            var colliderPoints = new List<Vector2>();

            if (collider is BoxCollider2D)
            {
                var boxCollider = collider as BoxCollider2D;
                colliderPoints = GetBoxColliderPoints(boxCollider);
            }

            if (collider is PolygonCollider2D)
            {
                var polygonCollider = collider as PolygonCollider2D;
                colliderPoints = GetPolygonColliderPoints(polygonCollider);
            }

            return colliderPoints;
        }

        private static List<Vector2> GetPolygonColliderPoints(PolygonCollider2D polygonCollider)
        {
            var points = new List<Vector2>();
            int pathCount = polygonCollider.pathCount;

            for (int pathIndex = 0; pathIndex < pathCount; pathIndex++)
            {
                Vector2[] pathPoints = polygonCollider.GetPath(pathIndex);

                int pointCount = pathPoints.Length;

                for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
                {
                    Vector2 point = polygonCollider.transform.TransformPoint(pathPoints[pointIndex]);
                    points.Add(point);
                }
            }
            return points;
        }
        private static List<Vector2> GetBoxColliderPoints(BoxCollider2D boxCollider)
        {
            var boxBounds = boxCollider.bounds;
            Vector2 topLeft = new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
            Vector2 topRight = new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
            Vector2 bottomRight = new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y);
            Vector2 bottomLeft = new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y);

            return new List<Vector2> { topLeft, topRight, bottomRight, bottomLeft };
        }
    }
}
