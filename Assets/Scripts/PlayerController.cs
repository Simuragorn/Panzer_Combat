using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnGameOver = new();
    public Vector2 TankPosition => tank.transform.position;

    [SerializeField] protected Tank tank;
    protected GameController gameController;
    protected bool isGameOver = false;

    protected virtual void Start()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnGameOver.AddListener(GameOver);
        tank.OnTankDestroyed.AddListener(GameOver);
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
            OnGameOver?.Invoke();
        }
    }
}
