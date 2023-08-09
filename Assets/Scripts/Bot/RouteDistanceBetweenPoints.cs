using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Bot
{
    public class RouteDistanceBetweenPoints
    {
        public int Point1Index { get; private set; }
        public int Point2Index { get; private set; }
        public float Distance { get; private set; }

        public RouteDistanceBetweenPoints(int point1Index, int point2Index, List<Vector2> routePoints)
        {
            Point1Index = point1Index;
            Point2Index = point2Index;

            Distance = Vector2.Distance(routePoints[point1Index], routePoints[point2Index]);
        }

    }
}
