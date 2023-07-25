using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] protected List<Track> tracks;
    [SerializeField] protected int originalTorque = 5;
    [SerializeField] protected Rigidbody2D tankRigidbody;
    protected int actualTorque;

    [SerializeField] protected Engine engine;
    [SerializeField] protected int originalPower = 15;
    protected int actualPower;

    protected void Awake()
    {
        actualTorque = originalTorque;
        actualPower = originalPower;
    }

    public void Move(float verticalAxisInput)
    {
        if (IsMovementBlocked)
        {
            return;
        }
        float forwardForce = verticalAxisInput * actualPower;
        Vector2 movementForce = tankRigidbody.transform.up * forwardForce;

        tankRigidbody.AddForce(movementForce, ForceMode2D.Force);
    }

    public void Rotate(float horizontalAxisInput)
    {
        if (IsMovementBlocked)
        {
            return;
        }

        float torqueForce = -(horizontalAxisInput * actualTorque);
        tankRigidbody.AddTorque(torqueForce, ForceMode2D.Force);
    }

    protected bool IsMovementBlocked =>
        engine.IsDamaged || tracks.Any(t => t.IsDamaged);
}
