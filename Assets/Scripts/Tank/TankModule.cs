using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class TankModule : Target
    {
        protected bool isDamaged = false;

        protected override void OnPenetrate()
        {
            base.OnPenetrate();
            isDamaged = true;
        }

        public bool IsDamaged => isDamaged;
    }
}
