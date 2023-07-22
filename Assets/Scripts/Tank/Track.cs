using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Track : TankModule
{
    [SerializeField] protected int originalTorque = 5;
    protected new Rigidbody2D rigidbody;

    protected int actualTorque;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody2D>();
        actualTorque = originalTorque;
    }

    public void Rotate(float horizontalAxisInput)
    {
        if (IsDamaged)
        {
            Debug.Log("Damaged");
            return;
        }

        float torqueForce = -(horizontalAxisInput * actualTorque);
        rigidbody.AddTorque(torqueForce, ForceMode2D.Force);
    }
}
