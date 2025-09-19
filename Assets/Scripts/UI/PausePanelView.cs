
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class PausePanelView : MonoBehaviour
{
    [SerializeField] private Button buttonClose;
    [SerializeField] private Button buttonResume;    
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonQuit;


    [SerializeField] private RectTransform panelRect;

   [SerializeField] private Animator animator;

    // Cache the hash of the pause state.
    int m_PauseEntryStateHash;



    /// <summary>
    /// Render Pause Screen
    /// </summary>
    public void Render()
    {
        m_PauseEntryStateHash = Animator.StringToHash("Base Layer.Entry");
        AddListeners();
        gameObject.SetActive(true);
        Invoke("PlayEntryAnimation", 0.2f);
       
    }

    private void AddListeners()
    {
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(OnCloseButtonClicke);

        buttonResume.onClick.RemoveAllListeners();
        buttonResume.onClick.AddListener(OnResumeButtonClicked);

        buttonRestart.onClick.RemoveAllListeners();
        buttonRestart.onClick.AddListener(OnRestartButtonClicked);

        buttonQuit.onClick.RemoveAllListeners();
        buttonQuit.onClick.AddListener(OnQuitButtonClicked);
    }

    private void PlayEntryAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(m_PauseEntryStateHash);
        }
    }


    /// <summary>
    /// Exit Game Play
    /// </summary>
    private void OnQuitButtonClicked()
    {
        DisablePanel();
    }

    /// <summary>
    /// Restart Game play
    /// </summary>
    private void OnRestartButtonClicked()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.stateHold);
        DisablePanel();
        GameController.GameRestartAction?.Invoke();
    }




    /// <summary>
    /// Resume GamePlay
    /// </summary>
    private void OnResumeButtonClicked()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.gameplayState);
        DisablePanel();
    }



    /// <summary>
    /// Close Pause Screen
    /// </summary>
    private void OnCloseButtonClicke()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.gameplayState);

        animator.Play("Base Layer.Exit");
       Invoke("DisablePanel",0.25f);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
