using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ӽ� ��ũ��Ʈ, UI �����ϴ� ��ũ��Ʈ�� �̵��� ����
public class SelectPopupUI : MonoBehaviour
{
    PopupUIManager popupUIManager = new PopupUIManager();

    public Button storeButton;
    public Button questButton;
    public Button healthButton; //������ �÷��̾� �ڵ����� �̵���Ű��. +�̶� ������ �̵��ϹǷ� enable = false �ʿ�.
    public Button closeButton;


    //�ҷ����� prefab���� GameObject�� �ƴ϶� script�� �ҷ����� ������ ���׸����� �����.
    public GameObject storePrefab;
    public GameObject questListPrefab;

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

        closeButton.onClick.AddListener(() => OffSelectPopup());
    }

    public void OffSelectPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnStore() //UIManager�� �ű� �� �Ű������� GameObject �ޱ�.-> ���׸����� ���� �Լ� �׳� ����ϱ�
    {
        if (storePrefab == null)
        {
            GameObject Sprefab = Resources.Load<GameObject>("PopUp/Store_Canvas"); 
            storePrefab = Instantiate(Sprefab);
        }
        storePrefab.SetActive(true);
    }

    public void OnQuestList() //UIManager�� �ű� �� �Ű������� GameObject �ޱ�.-> ���׸����� ���� �Լ� �׳� ����ϱ�
    {
        if (questListPrefab == null)
        {
            questListPrefab = PopupUIManager.Instance.OpenPopupUI<QuestListPopupUI>().gameObject;
        }
        questListPrefab.SetActive(true);
    }
}
