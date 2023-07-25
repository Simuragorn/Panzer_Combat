using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
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

        public float GetArmorThickness()
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
            if (shell.GetActualPenetration() >= effectiveArmor)
            {
                OnPenetrate();
                return ShellHitResultEnum.Penetrated;
            }

            if (IsRicochetHappened(hitAngle))
            {
                return ShellHitResultEnum.Ricochet;
            }
            return ShellHitResultEnum.ShellDestroyed;
        }

        protected bool IsRicochetHappened(float hitAngle)
        {
            if (hitAngle == 0)
            {
                hitAngle = 90;
            }
            int angleImpact = (int)(100 / (hitAngle / 10));

            var random = new System.Random();
            int randomRicochetPossibility = random.Next(0, 101);
            return randomRicochetPossibility + angleImpact > 70;
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
