using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void OnGameOver();
    public event OnGameOver onGameOver;

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

        human.onGameOver += GameOver;
    }

    protected void GameOver()
    {
        Debug.Log("Game Over");
        onGameOver?.Invoke();
    }
}
