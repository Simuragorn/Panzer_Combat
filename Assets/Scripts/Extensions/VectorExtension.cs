using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class VectorExtension
    {
        public static bool IsLaysBetweenPoints(this Vector3 targetPoint, Vector3 startPoint, Vector3 endPoint)
        {
            Vector3 endToStart = endPoint - startPoint;
            Vector3 targetToStart = targetPoint - startPoint;
            float dotValue = Vector3.Dot(targetToStart.normalized, endToStart.normalized);

            bool colinear = Math.Abs(1 - Math.Abs(dotValue)) < 0.1;
            if (!colinear)
                return false;

            float epsilon = 0.01f;
            bool suitableMagnitude = targetToStart.magnitude <= endToStart.magnitude + epsilon;
            if (!suitableMagnitude)
                return false;

            return true;
        }
    }
}
