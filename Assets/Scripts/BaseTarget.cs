using Assets.Scripts.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseTarget : MonoBehaviour
    {
        public Collider2D Collider;
        public float ArmorThickness;
        protected List<Vector2> colliderPoints;

        protected virtual void Start()
        {
            colliderPoints = Collider.GetColliderPoints();
        }

        public float GetArmorThickness()
        {
            return ArmorThickness;
        }

        public List<Vector2> GetColliderPoints()
        {
            return colliderPoints;
        }        

        public float GetEffectiveArmorThickness(float hitAngle)
        {
            float armorThickness = GetArmorThickness();
            float effectiveArmor = armorThickness / Mathf.Cos(hitAngle);
            return effectiveArmor;
        }

        public void OnPenetrate(BaseShell baseShell)
        {
            Debug.Log("Penetrated!");
        }
    }
}
