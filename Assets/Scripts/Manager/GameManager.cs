using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    IGameState currentState;

    public GamePauseState pauseState = new GamePauseState();
    public StateGamePlay gameplayState = new StateGamePlay();
    public StateHold stateHold = new StateHold();
    public StateGameOver stateGameOver = new StateGameOver();

    public IGameState GetCurrentState => currentState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.UpdateState(this);
    }

    public void ChangeState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }
}

public interface IGameState
{
    public void OnEnter();

    public void UpdateState(GameManager manager);

    public void OnExit();
}
