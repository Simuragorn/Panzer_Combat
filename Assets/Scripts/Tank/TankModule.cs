using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class TankModule : Target
    {
        public UnityEvent<bool> OnModuleStatusChanged;

        protected bool isDamaged = false;

        protected override void OnPenetrate()
        {
            base.OnPenetrate();
            isDamaged = true;
            OnModuleStatusChanged?.Invoke(isDamaged);
        }

        public bool IsDamaged => isDamaged;
    }
}
