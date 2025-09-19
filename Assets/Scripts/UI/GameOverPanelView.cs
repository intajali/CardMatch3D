
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameOverPanelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTotalScore;
    [SerializeField] private TextMeshProUGUI textTotalTime;

    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonRestart;

    [SerializeField] private Animator animator;

    /// <summary>
    /// Render Game over panel
    /// </summary>
    /// <param name="totalScore"></param>
    /// <param name="totalTime"></param>
    public void Render(int totalScore, float totalTime)
    {
        textTotalScore.text = string.Format("Total Score : {0}",totalScore);
        textTotalTime.text = string.Format("Total Time : {0}",totalTime);
        AddListeners();
        gameObject.SetActive(true);
        animator.Play("Base Layer.Entry");
    }

    private void AddListeners()
    {
        buttonExit.onClick.RemoveAllListeners();
        buttonExit.onClick.AddListener(OnExitButtonClicked);

        buttonRestart.onClick.RemoveAllListeners();
        buttonRestart.onClick.AddListener(OnRestartButtonClicked);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnRestartButtonClicked()
    {
        DisableView();
        GameManager.Instance.ChangeState(GameManager.Instance.stateHold);
        GameController.GameRestartAction?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnExitButtonClicked()
    {
        SceneManager.LoadScene(GameConstants.MENU_SCENE);
    }

    /// <summary>
    /// Disable Panel
    /// </summary>
    public void DisableView()
    {
        ResetData();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetData()
    {
        textTotalScore.text = string.Empty;
        textTotalTime.text = string.Empty;
    }
}
