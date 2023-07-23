using UnityEngine;

public class EngineController : MonoBehaviour
{
    [SerializeField] protected Engine engine;

    [SerializeField] protected int originalPower = 15;
    [SerializeField] protected Rigidbody2D tankRigidbody;
    protected int actualPower;

    protected void Awake()
    {
        actualPower = originalPower;
    }

    public void Move(float verticalAxisInput)
    {
        if (engine.IsDamaged)
        {
            return;
        }
        float forwardForce = verticalAxisInput * actualPower;
        Vector2 movementForce = tankRigidbody.transform.up * forwardForce;

        tankRigidbody.AddForce(movementForce, ForceMode2D.Force);
    }
}
