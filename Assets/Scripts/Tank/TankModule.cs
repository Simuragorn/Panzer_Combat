using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class TankModule : Target
    {
        protected bool IsDamaged = false;

        protected override void OnPenetrate()
        {
            IsDamaged = true;
        }
    }
}
