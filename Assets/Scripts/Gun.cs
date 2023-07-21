using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Shell ShellPrefab;
    public float CoolDownDelayInSeconds = 2;
    public Transform LaunchShellPosition;

    private DateTime LastShotDate;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryFire();
        }
    }

    private void TryFire()
    {
        TimeSpan difference = DateTime.Now - LastShotDate;
        if (difference.TotalSeconds > CoolDownDelayInSeconds)
        {
            var shell = Instantiate(ShellPrefab, LaunchShellPosition);
            shell.Launch();
            LastShotDate = DateTime.Now;
        }
    }
}
