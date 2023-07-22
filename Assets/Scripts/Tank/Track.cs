using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Track : MonoBehaviour
{
    [SerializeField] protected int originalTorque = 5;
    protected Rigidbody2D rigidbody;

    protected int actualTorque;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        actualTorque = originalTorque;
    }

    public void Rotate(float horizontalAxisInput)
    {
        float torqueForce = -(horizontalAxisInput * actualTorque);
        rigidbody.AddTorque(torqueForce, ForceMode2D.Force);
    }
}
