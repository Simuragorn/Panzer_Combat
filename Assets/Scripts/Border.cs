using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : Target
{
    protected override bool IsRicochetHappened(float hitAngle)
    {
        return false;
    }
}
