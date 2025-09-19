using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class GamePlayHUD : MonoBehaviour
{
    [SerializeField] private PausePanelView pausePanelView;


    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textTimer;

    [SerializeField] private Button pauseButton;

    private WaitForSeconds gameTimerDisplayWait = new WaitForSeconds(1f);

    /// <summary>
    /// Render View
    /// </summary>
    public void RenderView()
    {
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnPauseButtonClicked);

        textScore.text = "00";
        textTimer.text = "00";
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator UpdateTimer()
    {
        float time = 0;
        while (GameManager.Instance.GetCurrentState == GameManager.Instance.gameplayState)
        {
            time += 1;
            textTimer.text = time.ToString("0.00");
            yield return gameTimerDisplayWait;
        }

    }

    private void Update()
    {

    }



    /// <summary>
    /// Pause Button Clicked Action
    /// </summary>
    private void OnPauseButtonClicked()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.pauseState);
        pausePanelView.Render();
    }
}
