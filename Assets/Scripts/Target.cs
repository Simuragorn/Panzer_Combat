using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Target : MonoBehaviour
    {
        [SerializeField] protected float armorThickness = 30;
        protected new Collider2D collider;

        protected virtual void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        protected float GetArmorThickness()
        {
            return armorThickness;
        }

        public float GetEffectiveArmorThickness(float hitAngle)
        {
            float armorThickness = GetArmorThickness();
            float divider = Mathf.Cos(hitAngle * Mathf.Deg2Rad);
            float effectiveArmor = armorThickness / divider;
            return effectiveArmor;
        }

        public virtual ShellHitResultEnum HitAndGetResult(Shell shell, float hitAngle)
        {
            float effectiveArmor = GetEffectiveArmorThickness(hitAngle);
            float hitAdvantage = shell.GetActualPenetration() / effectiveArmor;
            if (hitAdvantage >= ShootingConstants.MinHitAdvantageForPenetration)
            {
                OnPenetrate();
                return ShellHitResultEnum.Penetrated;
            }
            if (hitAdvantage >= ShootingConstants.MinHitAdvantageForRicochet)
            {
                return ShellHitResultEnum.Ricochet;
            }
            return ShellHitResultEnum.ShellDestroyed;
        }

        protected virtual void OnPenetrate()
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
