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

    public GameLayout gameLayouts;

    public static UnityAction<Card> OnCardSelectAction;
    public static UnityAction<Card> OnCardDeleteAction;

    private Card previousSelectedCard = null;
    private Card currentSelectedCard = null;

    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        OnCardSelectAction += OnCardSelected;
        OnCardDeleteAction += OnCardDeleted;
        previousSelectedCard = null ;
        currentSelectedCard = null ;

        GameSetup();
    }


    public void GameSetup()
    {
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

    private void OnDisable()
    {
        OnCardSelectAction -= OnCardSelected;
        OnCardSelectAction -= OnCardDeleted;

    }

   
}
