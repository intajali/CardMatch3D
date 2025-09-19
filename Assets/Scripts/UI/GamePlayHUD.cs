using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class GamePlayHUD : MonoBehaviour
{
    [SerializeField] private PausePanelView pausePanelView;

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textTimer;

    [SerializeField] private Button pauseButton;

    private WaitForSeconds gameTimerDisplayWait = new WaitForSeconds(1f);

    private int currentScore = 0;
    private int turnCount = 0;
    private float totalTime = 0;

    public int CurrectScore => currentScore;
    public float TotalGamePlayTime => totalTime;
    public int TotalTurnCount => turnCount;


    /// <summary>
    /// Render View
    /// </summary>
    public void RenderView()
    {
        ResetData();
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnPauseButtonClicked);

        textScore.text = "0";
        textTimer.text = "0";
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator UpdateTimer()
    {
        while (GameManager.Instance.GetCurrentState == GameManager.Instance.gameplayState)
        {
            totalTime += 1;
            textTimer.text = totalTime.ToString();
            yield return gameTimerDisplayWait;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        UpdateTurnCount();
        currentScore += score;
        textScore.text = currentScore.ToString();
    }

    /// <summary>
    /// Update Turn taken 
    /// </summary>
    public void UpdateTurnCount()
    {
        turnCount++;
    }



    /// <summary>
    /// Pause Button Clicked Action
    /// </summary>
    private void OnPauseButtonClicked()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.pauseState);
        pausePanelView.Render();
    }

    /// <summary>
    /// Reset Data
    /// </summary>
    public void ResetData()
    {
        currentScore = 0;
        totalTime = 0;
        turnCount = 0;
    }
}
