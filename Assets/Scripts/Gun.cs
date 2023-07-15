using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Shell ShellPrefab;
    public int CoolDownDelayInSeconds = 2;
    public Transform LaunchShellPosition;

    private DateTime LastShotDate;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
