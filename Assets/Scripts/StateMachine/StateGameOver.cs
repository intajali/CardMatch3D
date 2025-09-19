

public class StateGameOver : IGameState
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
