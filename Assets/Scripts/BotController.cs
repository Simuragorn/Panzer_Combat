using UnityEngine;

public class BotController : PlayerController
{

    protected override void Shooting()
    {
        tank.Shoot();
    }

    protected override void Movement()
    {
        //tank.Rotate(horizontalAxis);
    }
}
