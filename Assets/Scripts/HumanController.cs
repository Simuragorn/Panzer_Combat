using UnityEngine;

public class HumanController : PlayerController
{

    protected KeyCode keyLeft;
    protected KeyCode keyRight;
    protected KeyCode keyTop;
    protected KeyCode keyDown;
    protected KeyCode keyShoot;

    public void Init(KeyCode playerKeyLeft, KeyCode playerKeyRight, KeyCode playerKeyTop, KeyCode playerKeyDown, KeyCode playerKeyShoot)
    {
        keyLeft = playerKeyLeft;
        keyRight = playerKeyRight;
        keyTop = playerKeyTop;
        keyDown = playerKeyDown;
        keyShoot = playerKeyShoot;

        tank.Init(true);
    }

    protected override void Shooting()
    {
        if (Input.GetKey(keyShoot))
        {
            tank.Shoot();
        }
    }

    protected override void Movement()
    {
        float verticalAxis = 0;
        if (Input.GetKey(keyTop))
        {
            verticalAxis = 1;
        }
        else if (Input.GetKey(keyDown))
        {
            verticalAxis = -1;
        }
        tank.Move(verticalAxis);

        float horizontalAxis = 0;
        if (Input.GetKey(keyRight))
        {
            horizontalAxis = 1;
        }
        else if (Input.GetKey(keyLeft))
        {
            horizontalAxis = -1;
        }
        if (verticalAxis < 0)
        {
            horizontalAxis *= -1;
        }
        tank.Rotate(horizontalAxis);
    }
}
