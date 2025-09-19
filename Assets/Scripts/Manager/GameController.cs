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
 
    public GameLayout gameLayouts;

    public static UnityAction<Card> OnCardSelectAction;
    public static UnityAction<Card> OnCardDeleteAction;

    public static UnityAction GameStartedAction;
    public static UnityAction GameRestartAction;

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

    public void GameSetup()
    {
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

        // Check for Match
        if (previousSelectedCard.CardID == currentSelectedCard.CardID)
        {
            SoundManager.Instance.PlayAudio(AudioType.CARD_MATCHED);
            previousSelectedCard.DeleteCard();
            currentSelectedCard.DeleteCard();
        }
        else 
        {
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
    }

    private void OnDisable()
    {
        OnCardSelectAction -= OnCardSelected;
        OnCardSelectAction -= OnCardDeleted;
        GameStartedAction -= OnGameStarted;

        GameRestartAction -= OnGameRestarted;


    }


}
