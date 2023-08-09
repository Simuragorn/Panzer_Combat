using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameOver = new();

    private HumanController human;
    private List<BotController> bots;

    private void Awake()
    {
        human = FindObjectOfType<HumanController>();
        bots = FindObjectsOfType<BotController>().ToList();
    }
    void Start()
    {
        human.Init(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.LeftControl);

        human.OnGameOver.AddListener(GameOver);
    }

    protected void GameOver()
    {
        Debug.Log("Game Over");
        OnGameOver?.Invoke();
    }
}
