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
        public Collider2D Collider => collider;

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
            if (hitAngle == 90)
            {
                return float.MaxValue;
            }
            float armorThickness = GetArmorThickness();
            float divider = Mathf.Cos(hitAngle * Mathf.Deg2Rad);
            float effectiveArmor = armorThickness / divider;
            return effectiveArmor;
        }

        public virtual ShellHitResultEnum HitAndGetResult(Shell shell, float impactAngle)
        {
            float effectiveArmor = GetEffectiveArmorThickness(impactAngle);
            if (shell.GetActualPenetration() >= effectiveArmor)
            {
                OnPenetrate();
                return ShellHitResultEnum.Penetrated;
            }

            if (IsRicochetHappened(impactAngle))
            {
                return ShellHitResultEnum.Ricochet;
            }
            return ShellHitResultEnum.ShellDestroyed;
        }

        protected virtual bool IsRicochetHappened(float impactAngle)
        {
            float hitAngle = 90 - Mathf.Abs(impactAngle);
            if (hitAngle == 0)
            {
                return true;
            }
            int angleImpact = (int)(100 / (hitAngle / 10));
            //Debug.Log(hitAngle);
            var random = new System.Random();
            int randomRicochetPossibility = random.Next(0, 101);
            return randomRicochetPossibility + angleImpact > 70;
        }

        protected virtual void OnPenetrate()
        {
            //Debug.Log("Penetrated!");
        }

        public List<Vector2> GetColliderPoints()
        {
            return collider.GetColliderPoints();
        }

        private void OnDrawGizmosSelected()
        {
            if (collider == null)
            {
                return;
            }
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
