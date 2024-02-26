using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�ӽ� ��ũ��Ʈ, UI �����ϴ� ��ũ��Ʈ�� �̵��� ����
public class SelectPopupUI : MonoBehaviour
{
    public GameObject selectPopupPrefab;//�ӽ�

    public Button storeButton;
    public Button questButton;
    public Button healthButton; //������ �÷��̾� �ڵ����� �̵���Ű��. +�̶� ������ �̵��ϹǷ� enable = false �ʿ�.
    public Button closeButton;

    public GameObject storePrefab;
    public GameObject questPrefab;

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
        selectPopupPrefab.SetActive(false);
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
        if (questPrefab == null)
        {
            GameObject Qprefab = Resources.Load<GameObject>("PopUp/QuestList_Canvas");
            storePrefab = Instantiate(Qprefab);
        }
        storePrefab.SetActive(true);
    }
}
