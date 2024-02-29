using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ӽ� ��ũ��Ʈ, UI �����ϴ� ��ũ��Ʈ�� �̵��� ����
public class SelectPopupUI : PopupUIBase
{
    public Button storeButton;
    public Button questButton;
    public Button healthButton; //������ �÷��̾� �ڵ����� �̵���Ű��. +�̶� ������ �̵��ϹǷ� enable = false �ʿ�.
    public Button closeButton;


    //������ UI�� SetActive�� ����ϱ� ���ؼ� GameObject�� ��������.
    public GameObject storePrefab;
    public GameObject questListPrefab;
    QuestListPopupUI questListPopup;
    StoreUI storeUI;

    private void Start()
    {
        StartButtonSetting();
    }

    //��ư ����--------------------------UI���� �� ��ũ��Ʈ�� �־��ִ� ��
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
