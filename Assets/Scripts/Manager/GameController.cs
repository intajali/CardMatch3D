using CardMatch.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameDataManager dataManager;
    [SerializeField] private GridManager gridManager;

    public GameLayout gameLayouts;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        GameSetup();
    }

    public void GameSetup()
    {
        gridManager.CreateGameBoard(gameLayouts.rowCount, gameLayouts.columnCount);
        gridManager.GenerateCardData(gameLayouts.rowCount, gameLayouts.columnCount, dataManager.GetCardData());
    }


    
}
