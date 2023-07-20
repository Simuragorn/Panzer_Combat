using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class VectorExtension
    {
        public static bool IsLaysBetweenPoints(this Vector3 targetPoint, Vector3 startPoint, Vector3 endPoint)
        {
            Vector3 endToStart = endPoint - startPoint;
            Vector3 targetToStart = targetPoint - startPoint;

            var crossVector = Vector3.Cross(targetToStart.normalized, endToStart.normalized);
            bool colinear = Mathf.Abs(crossVector.x) < 0.1 && Mathf.Abs(crossVector.y) < 0.1 && Mathf.Abs(crossVector.z) < 0.1;
            if (!colinear)
                return false;

            float epsilon = 0.0001f;
            bool suitableMagnitude = targetToStart.magnitude <= endToStart.magnitude + epsilon;

            if (!suitableMagnitude)
                return false;

            return true;
        }
    }
}
