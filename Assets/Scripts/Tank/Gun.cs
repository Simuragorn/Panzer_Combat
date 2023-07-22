using System;
using UnityEngine;

[RequireComponent(typeof(Tank))]
public class Gun : MonoBehaviour
{
    [SerializeField] protected Shell shellPrefab;
    [SerializeField] protected float coolDownDelayInSeconds = 1;
    [SerializeField] protected Transform launchShellPosition;
    protected Tank tank;

    protected Vector2 shootingDirection;

    protected DateTime lastShotDate;

    protected void Awake()
    {
        tank = GetComponent<Tank>();
        shootingDirection = Vector2.up;
    }

    public void TryShoot()
    {
        TimeSpan difference = DateTime.Now - lastShotDate;
        if (difference.TotalSeconds > coolDownDelayInSeconds)
        {
            var shell = Instantiate(shellPrefab, launchShellPosition.position, transform.localRotation);
            shell.Launch(shootingDirection, tank);
            lastShotDate = DateTime.Now;
        }
    }
}
