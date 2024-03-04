using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

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
    public Image blinkImage;
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



    //gameObject.SetActive(true) 상태에서 플레이어가 움직일 수 없으므로 움직임에 대한 부분은 따로 안해도 됨.
    public void OnMoveForHealth()
    {
        StartCoroutine(OnHealth());
        OffSelectPopupchildren();
    }

    //시간 부족..
    //화면 깜박임만 추가
    IEnumerator OnHealth()
    {
        Color color = blinkImage.color;
        while (blinkImage.color.a <= 1)
        {
            color.a += 3f / 255f;
            blinkImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (blinkImage.color.a >= 0)
        {
            color.a -= 1f / 255f;
            blinkImage.color = color;
            yield return null;
        }
        OnSelectPopupchildren();
        OffSelectPopup();
    }
}
