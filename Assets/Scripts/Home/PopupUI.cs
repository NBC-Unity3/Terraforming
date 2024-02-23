using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//임시 스크립트, UI 연결하는 스크립트로 이동할 예정
public class PopupUI : MonoBehaviour
{
    public GameObject popupPrefab;

    public Button storeButton;
    public Button questButton;
    public Button healthButton; //누르면 플레이어 자동으로 이동시키기. +이때 강제로 이동하므로 enable = false 필요.
    public Button closeButton;

    public GameObject storePrefab;
    public GameObject questPrefab;
    public GameObject[] quests;

    private void Start()
    {
        StartButtonSetting();
    }

    //버튼 연결
    public void StartButtonSetting()
    {

        storeButton.onClick.AddListener(() => OnStore());
        storeButton.onClick.AddListener(() => OffSelectPopup());

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OnStore() //UIManager로 옮길 시 매개변수로 GameObject 받기.
    {
        if (storePrefab == null)
        {
            GameObject Sprefab = Resources.Load<GameObject>("PopUp/Store_Canvas");
            storePrefab = Instantiate(Sprefab);
        }
        storePrefab.SetActive(true);
    }

    public void OffSelectPopup()
    {
        popupPrefab.SetActive(false);
    }
}
