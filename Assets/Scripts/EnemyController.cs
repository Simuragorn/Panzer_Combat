using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Tank tank;
    protected void FixedUpdate()
    {
        Shooting();
        Movement();
    }

    protected void Shooting()
    {
        tank.Shoot();
    }

    protected void Movement()
    {
        //tank.Rotate(horizontalAxis);
    }
}
