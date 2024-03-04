using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartMenu : PopupUIBase
{
    [SerializeField]
    private Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(OnStartBtnClick);
    }

    private void OnStartBtnClick()
    {
        SceneManager.LoadScene("LeeV2");
    }
}
