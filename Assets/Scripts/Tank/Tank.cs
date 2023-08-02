using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] protected List<Armor> armors;
    [SerializeField] protected MovementController movementController;
    [SerializeField] protected ShootingController gun;

    public delegate void OnTankDestroyed();
    public event OnTankDestroyed onTankDestroyed;
    protected bool isTankDestroyed = false;

    private void Start()
    {
        var vitalTankModules = GetComponentsInChildren<VitalTankModule>().ToList();
        vitalTankModules.ForEach(vitalModule => vitalModule.onModuleStatusChanged += VitalModuleStatusChanged);
    }

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

    protected void VitalModuleStatusChanged(bool isDamaged)
    {
        if (!isTankDestroyed)
        {
            onTankDestroyed?.Invoke();
            isTankDestroyed = true;
        }
    }
}
