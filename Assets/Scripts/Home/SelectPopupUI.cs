using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//임시 스크립트, UI 연결하는 스크립트로 이동할 예정
public class SelectPopupUI : MonoBehaviour
{
    public GameObject selectPopupPrefab;//임시

    public Button storeButton;
    public Button questButton;
    public Button healthButton; //누르면 플레이어 자동으로 이동시키기. +이때 강제로 이동하므로 enable = false 필요.
    public Button closeButton;

    public GameObject storePrefab;
    public GameObject questPrefab;

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

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OffSelectPopup()
    {
        selectPopupPrefab.SetActive(false);
    }

    public void OnStore() //UIManager로 옮길 시 매개변수로 GameObject 받기.-> 제네릭으로 만든 함수 그냥 사용하기
    {
        if (storePrefab == null)
        {
            GameObject Sprefab = Resources.Load<GameObject>("PopUp/Store_Canvas"); 
            storePrefab = Instantiate(Sprefab);
        }
        storePrefab.SetActive(true);
    }

    public void OnQuestList() //UIManager로 옮길 시 매개변수로 GameObject 받기.-> 제네릭으로 만든 함수 그냥 사용하기
    {
        if (questPrefab == null)
        {
            GameObject Qprefab = Resources.Load<GameObject>("PopUp/QuestList_Canvas");
            storePrefab = Instantiate(Qprefab);
        }
        storePrefab.SetActive(true);
    }
}
