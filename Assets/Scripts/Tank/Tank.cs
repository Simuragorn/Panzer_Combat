using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Tank : MonoBehaviour
{
    public UnityEvent OnTankDestroyed = new();
    public bool IsHuman => isHuman;

    [SerializeField] protected List<Armor> armors;
    [SerializeField] protected MovementController movementController;
    [SerializeField] protected ShootingController shootingController;
    [SerializeField] protected ViewController viewController;
    
    protected bool isHuman;
    protected bool isTankDestroyed = false;

    public void Init(bool isHuman)
    {
        this.isHuman = isHuman;
        viewController.Init(isHuman);
    }

    private void Start()
    {
        var vitalTankModules = GetComponentsInChildren<VitalTankModule>().ToList();
        vitalTankModules.ForEach(vitalModule => vitalModule.OnModuleStatusChanged.AddListener(VitalModuleStatusChanged));
    }

    public void Shoot()
    {
        shootingController.TryShoot();
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
            OnTankDestroyed?.Invoke();
            isTankDestroyed = true;
        }
    }
}
