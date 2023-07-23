using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] protected List<Armor> armors;
    [SerializeField] protected EngineController engine;
    [SerializeField] protected TrackController trackController;
    [SerializeField] protected GunController gun;

    public void Shoot()
    {
        gun.TryShoot();
    }

    public void Move(float verticalAxisInput)
    {
        engine.Move(verticalAxisInput);
    }

    public void Rotate(float horizontalAxisInput)
    {
        trackController.Rotate(horizontalAxisInput);
    }
}
