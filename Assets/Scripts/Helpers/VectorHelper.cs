using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class VectorHelper
    {
        public static Vector2? FindVectorThatCrossesTargetPoint(List<Vector2> vectorsPoints, Vector2 targetPoint)
        {
            for (int i = 0; i < vectorsPoints.Count; i++)
            {
                Vector2 point2 = vectorsPoints[i];
                Vector2 point1 = i == 0 ? vectorsPoints[^1] : vectorsPoints[i - 1];

                bool isCrosses = ((Vector3)targetPoint).IsLaysBetweenPoints(point1, point2);
                if (isCrosses)
                {
                    return point2 - point1;
                }
            }
            return null;
        }

        public static float GetRotationAxisToTarget(Vector2 targetPoint, Transform positionTansform)
        {
            Vector2 vectorToTarget = targetPoint - (Vector2)positionTansform.position;
            float angleWithRight = Vector2.Angle(positionTansform.right, vectorToTarget);
            float angleWithUp = Vector2.Angle(positionTansform.up, vectorToTarget);
            float horizontalAxis = angleWithRight > 90 ? -1 : 1;
            if (angleWithUp < 1)
            {
                horizontalAxis = 0;
            }
            return horizontalAxis;
        }
    }
}
