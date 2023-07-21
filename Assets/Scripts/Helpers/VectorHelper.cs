using System.Collections.Generic;
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
    }
}
