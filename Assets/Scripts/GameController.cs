using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<PlayerController> players;
    private List<EnemyController> enemies;

    private void Awake()
    {
        players = FindObjectsOfType<PlayerController>().ToList();
        enemies = FindObjectsOfType<EnemyController>().ToList();
    }
    void Start()
    {
        players[0].Init(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.LeftControl);
        if (players.Count > 1)
        {
            players[1].Init(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightControl);
        }
    }
}
