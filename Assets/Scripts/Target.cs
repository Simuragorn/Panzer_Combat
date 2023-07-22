using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Target : MonoBehaviour
    {
        [SerializeField] protected float armorThickness = 30;
        protected Collider2D collider;

        protected virtual void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        public float GetArmorThickness()
        {
            return armorThickness;
        }

        public float GetEffectiveArmorThickness(float hitAngle)
        {
            float armorThickness = GetArmorThickness();
            float effectiveArmor = armorThickness / Math.Abs(Mathf.Cos(hitAngle));
            return effectiveArmor;
        }

        public void OnPenetrate(Shell baseShell)
        {
            Debug.Log("Penetrated!");
        }

        public List<Vector2> GetColliderPoints()
        {
            return collider.GetColliderPoints();
        }

        private void OnDrawGizmosSelected()
        {
            var colliderPoints = collider.GetColliderPoints();
            if (!(colliderPoints?.Any() ?? false))
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
            for (int i = 1; i < colliderPoints.Count; i++)
            {
                Gizmos.color = colors[i - 1];
                Gizmos.DrawLine(colliderPoints[i - 1], colliderPoints[i]);
            }
            Gizmos.color = colors[colliderPoints.Count - 1];
            Gizmos.DrawLine(colliderPoints[colliderPoints.Count - 1], colliderPoints[0]);
        }
    }
}
