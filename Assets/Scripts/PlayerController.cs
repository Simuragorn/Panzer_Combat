using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Tank tank;

    protected void FixedUpdate()
    {
        Shooting();
        Movement();
    }

    protected void Shooting()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            tank.Shoot();
        }
    }

    protected void Movement()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        tank.Move(verticalAxis);

        float horizontalAxis = Input.GetAxis("Horizontal");
        if (verticalAxis < 0)
        {
            horizontalAxis *= -1;
        }
        tank.Rotate(horizontalAxis);
    }
}
