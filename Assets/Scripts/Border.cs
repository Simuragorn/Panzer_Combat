using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Border : MonoBehaviour, ITarget
{
    public BoxCollider2D Collider;

    public List<Vector2> GetColliderPoints()
    {
        return new List<Vector2>();
    }
}
