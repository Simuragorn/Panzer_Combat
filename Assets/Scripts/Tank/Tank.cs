using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] protected List<Armor> armors;
    [SerializeField] protected Engine engine;
    [SerializeField] protected Track track;
    [SerializeField] protected Gun gun;

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
        track.Rotate(horizontalAxisInput);
    }
}
