using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, GamePlay, Finish}
public class GameManager : Singleton<GameManager>
{

    private GameState state;
    // Start is called before the first frame update
    private void Awake()
    {
        //setup: tong quan game
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return state == gameState;
    }
}
