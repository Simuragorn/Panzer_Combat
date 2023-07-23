using System;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] protected Gun gun;

    [SerializeField] protected Shell shellPrefab;
    [SerializeField] protected float coolDownDelayInSeconds = 1;
    [SerializeField] protected Transform launchShellPosition;
    [SerializeField] protected Tank tank;

    protected DateTime lastShotDate;

    public void TryShoot()
    {
        if (gun.IsDamaged)
        {
            return;
        }
        TimeSpan difference = DateTime.Now - lastShotDate;
        if (difference.TotalSeconds > coolDownDelayInSeconds)
        {
            var shell = Instantiate(shellPrefab, launchShellPosition.position, transform.rotation);
            shell.Launch(tank);
            lastShotDate = DateTime.Now;
        }
    }
}
