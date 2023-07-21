using Assets.Scripts.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Hull : MonoBehaviour, ITarget
{
    public PolygonCollider2D PolygonCollider;
    private List<Vector2> colliderPoints;

    public List<Vector2> GetColliderPoints()
    {
        return colliderPoints;
    }

    private void Start()
    {
        colliderPoints = PolygonCollider.GetColliderPoints();
    }
}
