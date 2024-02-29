using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//임시 스크립트, UI 연결하는 스크립트로 이동할 예정
public class SelectPopupUI : PopupUIBase
{
    public Button storeButton;
    public Button questButton;
    public Button healthButton; //누르면 플레이어 자동으로 이동시키기. +이때 강제로 이동하므로 enable = false 필요.
    public Button closeButton;


    //생성한 UI를 SetActive로 사용하기 위해서 GameObject로 설정해줌.
    public GameObject storePrefab;
    public GameObject questListPrefab;
    QuestListPopupUI questListPopup;
    StoreUI storeUI;

    private void Start()
    {
        StartButtonSetting();
    }

    //버튼 연결--------------------------UI마다 각 스크립트에 넣어주는 것
    public void StartButtonSetting()
    {

        storeButton.onClick.AddListener(() => OnStore());
        storeButton.onClick.AddListener(() => OffSelectPopup());

        questButton.onClick.AddListener(() => OnQuestList());
        questButton.onClick.AddListener(() => OffSelectPopup());

        healthButton.onClick.AddListener(() => OffSelectPopup());
        healthButton.onClick.AddListener(() => OnMoveForHealth());

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OffSelectPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnSelectPopup()
    {
        gameObject.SetActive(true);
    }

    public void OnStore()
    {
        if (storePrefab == null)
        {
            storeUI = PopupUIManager.Instance.OpenPopupUI<StoreUI>();
            storeUI.closeBtn.onClick.AddListener(() => OnSelectPopup());
            storePrefab = storeUI.gameObject;
        }
        storePrefab.SetActive(true);
    }

    public void OnQuestList()
    {
        if (questListPrefab == null)
        {
            questListPopup = PopupUIManager.Instance.OpenPopupUI<QuestListPopupUI>();
            questListPopup.closeButton.onClick.AddListener(() => OnSelectPopup());
            questListPrefab = questListPopup.gameObject;
        }
        questListPrefab.SetActive(true);
    }

    public void OnMoveForHealth()
    {

    }
}
