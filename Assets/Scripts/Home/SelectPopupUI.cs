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
    [HideInInspector] public GameObject storePrefab;
    [HideInInspector] public GameObject questListPrefab;
    public GameObject selectBackground;
    QuestListPopupUI questListPopup;
    StoreUI storeUI;

    private void Start()
    {
        StartButtonSetting();
    }

    //버튼 연결--------------------------UI마다 각 스크립트에 넣어주는 것
    //AddListener은 각 한개씩
    public void StartButtonSetting()
    {

        storeButton.onClick.AddListener(() => OnStore());

        questButton.onClick.AddListener(() => OnQuestList());

        healthButton.onClick.AddListener(() => OnMoveForHealth());

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OffSelectPopupchildren()
    {
        selectBackground.SetActive(false);
    }

    public void OnSelectPopupchildren()
    {
        selectBackground.SetActive(true);
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
        OffSelectPopupchildren();
        if (storePrefab == null)
        {
            storeUI = PopupUIManager.Instance.OpenPopupUI<StoreUI>();
            storeUI.closeBtn.onClick.AddListener(() => OnSelectPopupchildren());
            storePrefab = storeUI.gameObject;
        }
        storePrefab.SetActive(true);
    }

    public void OnQuestList()
    {
        OffSelectPopupchildren();
        if (questListPrefab == null)
        {
            questListPopup = PopupUIManager.Instance.OpenPopupUI<QuestListPopupUI>();
            questListPopup.closeButton.onClick.AddListener(() => OnSelectPopupchildren());
            questListPrefab = questListPopup.gameObject;
        }
        questListPrefab.SetActive(true);
    }

    public void OnMoveForHealth()
    {
        OffSelectPopup();
    }
}
