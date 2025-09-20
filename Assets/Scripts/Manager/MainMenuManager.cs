using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;

    [SerializeField] private GameObject layoutItemPrefab;
    [SerializeField] private Transform layoutParent;
    [SerializeField] private ToggleGroup layoutToggleGroup;
    [SerializeField] private GameObject loadingObject;

    [Header("Container")]
    [SerializeField] private RectTransform TitleTextRect;
    [SerializeField] private RectTransform layoutViewRect;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonPlay;

    public static UnityAction<Vector2Int> LayoutSectedAction;

    private List<LayoutItem> generatedLayoutItems = new List<LayoutItem>();

    private Animator loadingAnimator;
    /// <summary>
    /// Start will called only once 
    /// </summary>
    private void Awake()
    {
        loadingAnimator = loadingObject.GetComponent<Animator>();
        loadingObject.SetActive(true);

        loadingAnimator.Play("Base Layer.Loading");
        GenerateLayoutItems();
        AddListeners();
        Invoke("MenuItemStartAnimation", 1f);
        //MenuItemStartAnimation();
    }

    private void AddListeners()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonPlay.onClick.AddListener(OnPlayButtonClicked);

        buttonSettings.onClick.RemoveAllListeners();
        buttonSettings.onClick.AddListener(OnSettingButtonClicked);
    }

    /// <summary>
    /// Action On Setting Button Clicked.
    /// </summary>
    private void OnSettingButtonClicked()
    {

    }


    /// <summary>
    /// Action on Play Button Clicked
    /// </summary>
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(GameConstants.GAMEPLAY_SCENE);
    }


    private void OnEnable()
    {
        LayoutSectedAction += OnLayoutSelected;
    }



    /// <summary>
    /// Generate Layout Items.
    /// </summary>
    private void GenerateLayoutItems()
    {

        for (int i = 0; i < gameDataManager.gameLayouts.Count; i++)
        {
            if (generatedLayoutItems.Count != gameDataManager.gameLayouts.Count)
            {
                LayoutItem item = CreatePrefab();
                item.Render(i,gameDataManager.gameLayouts[i].rowCount, gameDataManager.gameLayouts[i].columnCount, layoutToggleGroup);
                generatedLayoutItems.Add(item);
            }
        }
        if(gameDataManager.selectedGameLayout.rowCount == 0 || gameDataManager.selectedGameLayout.columnCount == 0)
            return;
       LayoutItem selectedItem = generatedLayoutItems.Where(t=> t.Rows.Equals(gameDataManager.selectedGameLayout.rowCount) &&
                                    t.Columns.Equals(gameDataManager.selectedGameLayout.columnCount)).FirstOrDefault();

        if (selectedItem != null)
        {
            selectedItem.SetEnable();
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

    private void MenuItemStartAnimation()
    {
        loadingObject.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(TitleTextRect.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InOutSine))
            .Append(layoutViewRect.DOAnchorPosX(0f, 0.3f).SetEase(Ease.InOutSine))
            .Append(buttonSettings.GetComponent<RectTransform>().DOAnchorPosX(390f, 0.3f).SetEase(Ease.InOutSine))
            .Join(buttonPlay.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.3f).SetEase(Ease.InOutSine));
    }

    /// <summary>
    /// Selected LayoutData
    /// </summary>
    /// <param name="arg0"></param>
    private void OnLayoutSelected(Vector2Int layoutData)
    {
        gameDataManager.UpdateSelectedGameLayput(layoutData);
    }

    private void OnDisable()
    {
        LayoutSectedAction -= OnLayoutSelected;

    }
}
