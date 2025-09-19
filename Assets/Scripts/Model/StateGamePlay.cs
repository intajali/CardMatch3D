using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGamePlay : IGameState
{
    public void OnEnter()
    {
       
    }

    public void OnExit()
    {
       
    }

    public void UpdateState(GameManager manager)
    {
        manager.ChangeState(this);
    }
}
