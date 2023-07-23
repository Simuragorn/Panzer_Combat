using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<PlayerController> players;
    void Start()
    {
        players[0].Init(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.LeftControl);
        players[1].Init(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightControl);
    }
}
