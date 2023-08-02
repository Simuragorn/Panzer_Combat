using Assets.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TankStatus : MonoBehaviour
{
    [SerializeField] private Image engineCheckMark;
    [SerializeField] private Image trackCheckMark;
    [SerializeField] private Image gunCheckMark;
    [SerializeField] private Image ammoRackCheckMark;
    [SerializeField] private Image fuelTankCheckMark;
    private HumanController playerController;
    void Start()
    {
        playerController = FindObjectOfType<HumanController>();
        var playerTank = playerController.GetComponentInChildren<Tank>();
        var tankModules = playerTank.GetComponentsInChildren<TankModule>().ToList();

        tankModules.ForEach(module =>
        {
            if (module is Track)
            {
                ObserveTrackModule(module as Track);
                return;
            }
            if (module is Engine)
            {
                ObserveEngineModule(module as Engine);
                return;
            }
            if (module is Gun)
            {
                ObserveGunModule(module as Gun);
                return;
            }
            if (module is AmmoRack)
            {
                ObserveAmmoRackModule(module as AmmoRack);
                return;
            }
            if (module is FuelTank)
            {
                ObserveFuelTankModule(module as FuelTank);
                return;
            }
        });
    }

    protected void TrackStatusChanged(bool isDamaged)
    {
        trackCheckMark.enabled = !isDamaged;
    }

    protected void EngineStatusChanged(bool isDamaged)
    {
        engineCheckMark.enabled = !isDamaged;
    }

    protected void GunStatusChanged(bool isDamaged)
    {
        gunCheckMark.enabled = !isDamaged;
    }

    protected void AmmoRackStatusChanged(bool isDamaged)
    {
        ammoRackCheckMark.enabled = !isDamaged;
    }

    protected void FuelTankStatusChanged(bool isDamaged)
    {
        fuelTankCheckMark.enabled = !isDamaged;
    }


    protected void ObserveTrackModule(Track track)
    {
        track.onModuleStatusChanged += TrackStatusChanged;
    }

    protected void ObserveEngineModule(Engine engine)
    {
        engine.onModuleStatusChanged += EngineStatusChanged;
    }

    protected void ObserveGunModule(Gun gun)
    {
        gun.onModuleStatusChanged += GunStatusChanged;
    }

    protected void ObserveAmmoRackModule(AmmoRack ammoRack)
    {
        ammoRack.onModuleStatusChanged += AmmoRackStatusChanged;
    }

    protected void ObserveFuelTankModule(FuelTank fuelTank)
    {
        fuelTank.onModuleStatusChanged += FuelTankStatusChanged;
    }
}
