using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ӽ� ��ũ��Ʈ, UI �����ϴ� ��ũ��Ʈ�� �̵��� ����
public class PopupUI : MonoBehaviour
{
    public GameObject popupPrefab;

    public Button storeButton;
    public Button questButton;
    public Button healthButton; //������ �÷��̾� �ڵ����� �̵���Ű��. +�̶� ������ �̵��ϹǷ� enable = false �ʿ�.
    public Button closeButton;

    public GameObject storePrefab;
    public GameObject questPrefab;
    public GameObject[] quests;

    private void Start()
    {
        StartButtonSetting();
    }

    //��ư ����
    public void StartButtonSetting()
    {

        storeButton.onClick.AddListener(() => OnStore());
        storeButton.onClick.AddListener(() => OffSelectPopup());

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OnStore() //UIManager�� �ű� �� �Ű������� GameObject �ޱ�.
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
