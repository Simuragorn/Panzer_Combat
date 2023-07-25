using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] protected List<Armor> armors;
    [SerializeField] protected MovementController movementController;
    [SerializeField] protected GunController gun;

    public void Shoot()
    {
        gun.TryShoot();
    }

    public void Move(float verticalAxisInput)
    {
        movementController.Move(verticalAxisInput);
    }

    public void Rotate(float horizontalAxisInput)
    {
        movementController.Rotate(horizontalAxisInput);
    }
}
