using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class ColliderExtension
    {
        public static Vector2 GetNormalTowardsCenter(this Collider2D collider, Vector2 contactPoint, Vector2 sideVector)
        {
            var sideNormalVector1 = new Vector2(sideVector.y, -sideVector.x);
            var sideNormalVector2 = new Vector2(-sideVector.y, sideVector.x);

            float offset = 0.1f;
            var neededNormal = sideNormalVector1;
            var offsetPoint = contactPoint + sideNormalVector2 * offset;
            if (collider.bounds.Contains(offsetPoint))
            {
                neededNormal = sideNormalVector2;
            }
            return neededNormal;
        }

        public static Vector2 GetColliderTopPoint(this Collider2D collider)
        {
            if (collider is CapsuleCollider2D)
            {
                var capsuleCollider = collider as CapsuleCollider2D;
                var topPoint = (Vector2)capsuleCollider.transform.position + Vector2.up * (capsuleCollider.size.y * 0.5f);
                return topPoint;
            }
            if (collider is BoxCollider2D)
            {
                var boxCollider = collider as BoxCollider2D;
                var topPoint = (Vector2)boxCollider.transform.position + Vector2.up * (boxCollider.size.y * 0.5f);
                return topPoint;
            }
            throw new System.Exception("Unknown collider type");
        }

        public static List<Vector2> GetColliderPoints(this Collider2D collider)
        {
            if (collider is BoxCollider2D)
            {
                var boxCollider = collider as BoxCollider2D;
                return GetBoxColliderPoints(boxCollider);
            }

            if (collider is PolygonCollider2D)
            {
                var polygonCollider = collider as PolygonCollider2D;
                return GetPolygonColliderPoints(polygonCollider);
            }
            throw new System.Exception("Unknown collider type");
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
