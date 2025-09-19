
using CardMatch.DataModel;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int CardID;
    [SerializeField] private Image cardBG;
    [SerializeField] private Image cardIcon;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite cardFrontSprite;

    [SerializeField] private Button cardButton;
    [SerializeField] private RectTransform cardRectTransform;

    public RectTransform cardRect => cardRectTransform;

    public bool IsCardSelected;

    /// <summary>
    /// Render Card
    /// </summary>
    /// <param name="cardData"></param>
    public void RenderCard(CardDataModel cardData)
    {
        if (cardData == null)
        {
            Debug.LogError("Card Data should not be NULL.");
            return;
        }

        CardID = cardData.cardID;
        cardBG.sprite = cardFrontSprite;
        cardIcon.sprite = cardData.cardIcon;
        cardIcon.enabled = true;
        AddListeners();
        gameObject.SetActive(true);

    }

    /// <summary>
    /// 
    /// </summary>
    private void AddListeners()
    {
        cardButton.onClick.RemoveAllListeners();
        cardButton.onClick.AddListener(OnCardSelected);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCardSelected()
    {
        if (!IsCardSelected)
        {
            ShowCard();
        }
    }

    /// <summary>
    /// Show card view with Icon
    /// </summary>
    public void ShowCard()
    {
        IsCardSelected = true;
        SoundManager.Instance.PlayAudio(AudioType.CARD_FLIP);
        cardRectTransform.DORotate(new Vector3(0, 90, 0), 0.25f).SetEase(Ease.InOutExpo).
                OnComplete(() =>
                {
                    cardBG.sprite = cardFrontSprite;
                    cardIcon.enabled = true;
                    cardRectTransform.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.InOutExpo).
                    OnComplete(() => GameController.OnCardSelectAction?.Invoke(this));
                });
    }


    /// <summary>
    /// Disable Card view
    /// </summary>
    public void HideCard()
    {
        cardRectTransform.DORotate(new Vector3(0, 90, 0), 0.25f).SetEase(Ease.InOutExpo).
                OnComplete(() =>
                {
                    cardBG.sprite = cardBackSprite;
                    cardIcon.enabled = false;
                    cardRectTransform.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.InOutExpo);
                });

        IsCardSelected = false;
    }

    /// <summary>
    /// Delete Card gameobject 
    /// </summary>
    public void DeleteCard()
    {
        cardRectTransform.DOScale(0f, 0.1f).SetEase(Ease.OutFlash).OnComplete(() =>
        {
            GameController.OnCardDeleteAction?.Invoke(this);
        });
    }
}
