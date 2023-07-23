using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    [SerializeField] protected List<Track> tracks;

    [SerializeField] protected int originalTorque = 5;
    [SerializeField] protected Rigidbody2D tankRigidbody;

    protected int actualTorque;

    protected void Awake()
    {
        actualTorque = originalTorque;
    }

    public void Rotate(float horizontalAxisInput)
    {
        if (tracks.Any(t => t.IsDamaged))
        {
            return;
        }

        float torqueForce = -(horizontalAxisInput * actualTorque);
        tankRigidbody.AddTorque(torqueForce, ForceMode2D.Force);
    }
}
