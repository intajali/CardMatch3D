using CardMatch.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelData", menuName = "CardMatch3D/GameLevelData")]
public class GameDataManager : ScriptableObject
{
    public List<GameLevelData> gameLevels;



    /// <summary>
    /// Get all Card Data
    /// </summary>
    /// <returns></returns>
    public List<CardDataModel> GetCardData()
    {
        return gameLevels[0].cardDataModels;
    }
}

[System.Serializable]
public class GameLevelData
{
    public List<CardDataModel> cardDataModels;
}
