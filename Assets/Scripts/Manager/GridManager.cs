using CardMatch.DataModel;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private RectTransform gridStartPoint;
    [SerializeField] private RectTransform gridRectTransform;
    [SerializeField] private Image gridLayoutImage;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;

    private float cardWidth;
    private float cardHeight;

    [SerializeField] private Vector2 spacing = new Vector2(10, 10);
    [SerializeField] private Vector2 padding = new Vector2(10, 10);

    private List<Vector2> gridPositions = new List<Vector2>();
    private List<Card> generatedCardList = new List<Card>();
    private List<CardDataModel> cardDatas = new List<CardDataModel>();

    private WaitForSeconds cardGenerationDelay = new WaitForSeconds(0.5f);
    private Coroutine cardGenerateRoutine = null;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    public void CreateGameBoard(int rows, int columns)
    {

        if(rows == 0 || columns == 0) { return; }

        if(rows * columns % 2 != 0) {
            Debug.LogError("Grid Size not matched. Must be divisable by 2");
            return;
        }

        gridPositions.Clear();

        float gridWidth = gridRectTransform.rect.width;
        float gridHeight = gridRectTransform.rect.height;

        cardWidth = gridStartPoint.rect.width + spacing.x;
        cardHeight = gridStartPoint.rect.height + spacing.y;

        gridWidth = rows * (cardWidth + spacing.x) + padding.x*2;
        gridHeight = columns * (cardHeight + spacing.y) + padding.y*2;

        gridRectTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(gridWidth, gridHeight);

        float startX = -gridWidth / 2 + (cardWidth / 2 + spacing.x);
        float startY = gridHeight / 2 - (cardHeight / 2 + spacing.y);

        for (int c = 0; c < columns; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                Vector2 gridPosition = new Vector2(startX + r * (cardWidth + spacing.x), startY - c * (cardHeight + spacing.y));
                gridPositions.Add(gridPosition);
            }
        }
        gridLayoutImage.enabled = true;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rowCount"></param>
    /// <param name="columnCount"></param>
    /// <param name="cards"></param>
    public void GenerateCardData(int rowCount, int columnCount, List<CardDataModel> cards)
    {
        cardDatas.Clear();
        int requiredCardCount = (rowCount * columnCount) / 2;

       // List<int> randomSpriteIndex = GetRandomUniqueNumbers(cards, requiredCardCount);

        for (int i = 0; i < requiredCardCount; i++)
        {
            cardDatas.Add(cards[i]);
            cardDatas.Add(cards[i]);
        }

        ShuffleGeneratedCards();

        if (cardGenerateRoutine != null)
        {
            StopCoroutine(cardGenerateRoutine);
            cardGenerateRoutine = null;
        }

        cardGenerateRoutine = StartCoroutine(StartCreatingCards());


    }


    /// <summary>
    /// Randomise Cards
    /// </summary>
    private void ShuffleGeneratedCards()
    {
        var count = cardDatas.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var randomIndex = Random.Range(i, count);
            var temp = cardDatas[i];
            cardDatas[i] = cardDatas[randomIndex];
            cardDatas[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCreatingCards()
    {
        yield return cardGenerationDelay;
        generatedCardList = new List<Card>();
        for (var i = 0; i < cardDatas.Count; i++)
        {
            Card newCard = GenerateNewCard(i);
            newCard.RenderCard(cardDatas[i]);

            generatedCardList.Add(newCard);

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        HideCards();

    }

    /// <summary>
    /// Generate Cards
    /// </summary>
    public Card GenerateNewCard(int positionIndex)
    {
        Card newCard = Utils.Generate<Card>(cardPrefab, cardParent) as Card;
        newCard.GetComponent<RectTransform>().anchoredPosition = gridPositions[positionIndex];
        newCard.transform.localScale = Vector3.one;

        return newCard;
    }

    /// <summary>
    /// Hide All cards
    /// </summary>
    private void HideCards()
    {
        if (generatedCardList == null) return;
        for (int i = 0; i < generatedCardList.Count; i++)
        {
            generatedCardList[i].HideCard();
        }
        GameController.GameStartedAction?.Invoke();
    }

    /// <summary>
    /// Delete Card
    /// </summary>
    public void DeleteCard(Card card)
    {
        if(generatedCardList.Count > 0)
        {
            generatedCardList.Remove(card);
        }

        // Check for Game Over
        if(generatedCardList.Count == 0 )
        {
            GameController.GameOverAction?.Invoke();
        }
    }

    /// <summary>
    /// Clear all Cards
    /// </summary>
    public void ClearCards()
    {
        if (generatedCardList == null) return;

        foreach (Card card in generatedCardList)
        {
          DestroyImmediate(card.gameObject);
        }
        generatedCardList.Clear();
    }

    public void ResetGrid()
    {
        gridLayoutImage.enabled = false;
        generatedCardList.Clear();
        gridPositions.Clear();
        cardDatas.Clear();
    }

    /// <summary>
    /// Generate Unique Random Numbers
    /// </summary>
    /// <param name="cardDataModels"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private List<int> GetRandomUniqueNumbers(List<CardDataModel> cardDataModels , int length)
    {
        List<int> uniqueRandom = new List<int>();

        while (length != uniqueRandom.Count)
        {
            int index = Random.Range(0, cardDataModels.Count);
            if(!uniqueRandom.Contains(index))
            {
                uniqueRandom.Add(index);
            }

        }
        return uniqueRandom;
    }
}
