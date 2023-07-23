using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
            float top = boxCollider.offset.y + (boxCollider.size.y / 2f);
            float btm = boxCollider.offset.y - (boxCollider.size.y / 2f);
            float left = boxCollider.offset.x - (boxCollider.size.x / 2f);
            float right = boxCollider.offset.x + (boxCollider.size.x / 2f);

            Vector2 topLeft = boxCollider.transform.TransformPoint(new Vector2(left, top));
            Vector2 topRight = boxCollider.transform.TransformPoint(new Vector2(right, top));
            Vector2 bottomLeft = boxCollider.transform.TransformPoint(new Vector2(left, btm));
            Vector2 bottomRight = boxCollider.transform.TransformPoint(new Vector2(right, btm));
            return new List<Vector2> { topLeft, topRight, bottomRight, bottomLeft };
        }
    }
}
