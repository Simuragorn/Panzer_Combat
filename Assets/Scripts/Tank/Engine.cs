using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Engine : TankModule
{
    [SerializeField] protected int originalPower = 15;
    protected new Rigidbody2D rigidbody;
    protected int actualPower;

    protected override void Awake()
    {
        base.Awake();
        rigidbody = GetComponent<Rigidbody2D>();
        actualPower = originalPower;
    }

    public void Move(float verticalAxisInput)
    {
        if (IsDamaged)
        {
            Debug.Log("Damaged");
            return;
        }
        float forwardForce = verticalAxisInput * actualPower;
        Vector2 movementForce = rigidbody.transform.up * forwardForce;

        rigidbody.AddForce(movementForce, ForceMode2D.Force);
    }
}
