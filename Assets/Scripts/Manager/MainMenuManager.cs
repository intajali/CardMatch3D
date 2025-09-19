using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;

    [SerializeField] private GameObject layoutItemPrefab;
    [SerializeField] private Transform layoutParent;

    private void Start()
    {
        GenerateLayoutItems();
    }

    private void GenerateLayoutItems()
    {
        for (int i = 0; i < gameDataManager.gameLayouts.Count; i++)
        {
            LayoutItem item = CreatePrefab();
            item.Render(gameDataManager.gameLayouts[i].rowCount, gameDataManager.gameLayouts[i].columnCount);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private LayoutItem CreatePrefab()
    {
       LayoutItem newItem = Utils.Generate<LayoutItem>(layoutItemPrefab, layoutParent) as LayoutItem;
       newItem.transform.localScale = Vector3.one;
        return newItem;
    }
}
