using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseState : IGameState
{
    public void OnEnter()
    {
        Time.timeScale = 0.0f;
    }

    public void OnExit()
    {
        Time.timeScale = 1.0f;
    }

    public void UpdateState(GameManager manager)
    {
        manager.ChangeState(this);
    }
}
