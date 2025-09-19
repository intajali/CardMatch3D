
using CardMatch.DataModel;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int CardID;
    [SerializeField] private Image cardBG;
    [SerializeField] private Image cardIcon;
    [SerializeField] private Sprite cardBackSprite;
    [SerializeField] private Sprite cardFrontSprite;

    /// <summary>
    /// Render Card
    /// </summary>
    /// <param name="cardData"></param>
    public void RenderCard(CardDataModel cardData)
    {
        
    }

}
