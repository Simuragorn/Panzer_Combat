using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Engine : MonoBehaviour
{
    [SerializeField] protected int originalPower = 15;
    protected new Rigidbody2D rigidbody;
    protected int actualPower;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        actualPower = originalPower;
    }

    public void Move(float verticalAxisInput)
    {
        float forwardForce = verticalAxisInput * actualPower;
        Vector2 movementForce = rigidbody.transform.up * forwardForce;

        rigidbody.AddForce(movementForce, ForceMode2D.Force);
    }
}
