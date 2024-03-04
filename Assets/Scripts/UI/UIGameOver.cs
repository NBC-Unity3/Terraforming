using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : PopupUIBase
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button endBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(OnRestartBtnClick);
        endBtn.onClick.AddListener(OnEndBtnClick);
    }

    private void OnRestartBtnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void OnEndBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
