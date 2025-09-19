using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardMatch.DataModel
{

    /// <summary>
    /// Model for Card Data
    /// </summary>
    [System.Serializable]
    public class CardDataModel
    {
        public int cardID;
        public Sprite cardIcon;
    }

    /// <summary>
    /// Model for Grid Layout
    /// </summary>
    [System.Serializable]
    public class GameLayout
    {
        public int rowCount;
        public int columnCount;
    }
}
