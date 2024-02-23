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
    public Button healthButton;
    public Button closeButton;

    public GameObject storePrefab;
    public GameObject questPrefab;
    public GameObject HealthPrefab; //회복을 누르면 바로 회복하러 이동할지 선택

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

    public void OnStore()
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
