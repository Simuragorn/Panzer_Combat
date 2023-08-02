using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnGameOver();
    public event OnGameOver onGameOver;
    public Vector2 TankPosition => tank.transform.position;

    [SerializeField] protected Tank tank;
    protected GameController gameController;
    protected bool isGameOver = false;

    protected virtual void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.onGameOver += GameOver;
        tank.onTankDestroyed += GameOver;
    }

    protected void FixedUpdate()
    {
        if (isGameOver)
        {
            return;
        }
        Shooting();
        Movement();
    }

    protected virtual void Shooting()
    {
    }

    protected virtual void Movement()
    {

    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            onGameOver?.Invoke();
        }
    }
}
