using CardMatch.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameDataManager dataManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GamePlayHUD gamePlayHUD;
    [SerializeField] private GameOverPanelView gameOverPanelView;
 
    public GameLayout gameLayouts;

    public static UnityAction<Card> OnCardSelectAction;
    public static UnityAction<Card> OnCardDeleteAction;

    public static UnityAction GameStartedAction;
    public static UnityAction GameRestartAction;
    public static UnityAction GameOverAction;

    private Card previousSelectedCard = null;
    private Card currentSelectedCard = null;

    private Coroutine gameTimerRoutine = null;

    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        gridManager.ResetGrid();
        OnCardSelectAction += OnCardSelected;
        OnCardDeleteAction += OnCardDeleted;
        GameRestartAction += OnGameRestarted;
        GameStartedAction += OnGameStarted;
        GameOverAction += OnGameOver;

        previousSelectedCard = null ;
        currentSelectedCard = null ;

        GameSetup();
    }

   

    /// <summary>
    /// Notify on game started
    /// </summary>
    private void OnGameStarted()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.gameplayState);
        if(gameTimerRoutine  != null)
            StopCoroutine(gameTimerRoutine);
        gameTimerRoutine = StartCoroutine( gamePlayHUD.UpdateTimer());
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameSetup()
    {
        gameLayouts = dataManager.selectedGameLayout;
        gamePlayHUD.RenderView();
        gridManager.CreateGameBoard(gameLayouts.rowCount, gameLayouts.columnCount);
        gridManager.GenerateCardData(gameLayouts.rowCount, gameLayouts.columnCount, dataManager.GetCardData());
    }

    /// <summary>
    /// On Card Selected Callback
    /// </summary>
    /// <param name="arg0"></param>
    private void OnCardSelected(Card card)
    {
        currentSelectedCard = card;

        if (previousSelectedCard == null)
        {
            previousSelectedCard = card;
            return;
        }

        // Check for Match Card
        if (previousSelectedCard.CardID == currentSelectedCard.CardID)
        {
            SoundManager.Instance.PlayAudio(AudioType.CARD_MATCHED);
            previousSelectedCard.DeleteCard();
            currentSelectedCard.DeleteCard();
            gamePlayHUD.UpdateScore(10);
            
        }
        else 
        {
            gamePlayHUD.UpdateTurnCount();
            SoundManager.Instance.PlayAudio(AudioType.CARD_UNMATCHED);
            previousSelectedCard.HideCard();
            currentSelectedCard.HideCard();
        }

        previousSelectedCard = null;
        currentSelectedCard = null;
    }

    /// <summary>
    /// On Card Delete Callback
    /// </summary>
    /// <param name="arg0"></param>
    private void OnCardDeleted(Card card)
    {
        gridManager.DeleteCard(card);
    }

    /// <summary>
    /// On Game Restarted.
    /// </summary>
    private void OnGameRestarted()
    {
        gridManager.ClearCards();
        gridManager.ResetGrid();
        Invoke("GameSetup",0.25f);

    }

    /// <summary>
    /// 
    /// </summary>
    private void OnGameOver()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.stateGameOver);
        gameOverPanelView.Render(gamePlayHUD.CurrectScore, gamePlayHUD.TotalGamePlayTime);
    }

    private void OnDisable()
    {
        OnCardSelectAction -= OnCardSelected;
        OnCardSelectAction -= OnCardDeleted;
        GameStartedAction -= OnGameStarted;
        GameOverAction -= OnGameOver;
        GameRestartAction -= OnGameRestarted;


    }


}
