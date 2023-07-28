using Assets.Scripts;
using Assets.Scripts.Enums;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : Target
{
    public override ShellHitResultEnum HitAndGetResult(Shell shell, float hitAngle)
    {
        return ShellHitResultEnum.ShellDestroyed;
    }
}
