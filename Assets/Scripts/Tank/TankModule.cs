using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class TankModule : Target
    {
        public delegate void OnModuleStatusChanged(bool isDamaged);
        public event OnModuleStatusChanged onModuleStatusChanged;

        protected bool isDamaged = false;

        protected override void OnPenetrate()
        {
            base.OnPenetrate();
            isDamaged = true;
            onModuleStatusChanged?.Invoke(isDamaged);
        }

        public bool IsDamaged => isDamaged;
    }
}
