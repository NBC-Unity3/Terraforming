using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPopupUI : PopupUIBase
{
    public Button[] questTitleButton = new Button[4]; //�迭�� �ؼ� QuestPrefab�� �ִ� title�� ����. ������ �ε����� �´� Canvasȭ�� �����ֱ�
    public Button closeButton; //�ڷ� ���� ������ �� �� UI�� ������ ���� �ƴϸ� �ƿ� ��������.

    public Transform questListPosition;
    public QuestList[] questList = new QuestList[4];

    //QuestList��ư�� ������ Quest �������� ���� ���� �����ǵ��� ����. -> Quest ��� ����
    //Quest �������� ��� �ٲ�� �ȵ�. Quest ������ ������ ���� ���ϸ� ���� ���� �ʿ�.
    public GameObject questPrefab; // -> Quest ������ ������ �����ִ� Quest_canvas. List�� ���� quest ����

    private void Start()
    {
        StartButtonSetting();
        for (int i = 0; i < 4; i++)
        {
            questList[i] = PopupUIManager.Instance.OpenPopupUI<QuestList>();
            questList[i].transform.parent = questListPosition.transform;
        }
    }

    public void StartButtonSetting()
    {
        //questTitleButton.onClick.AddListener(() => OnQuest());

        closeButton.onClick.AddListener(() => OffQuestListPopup());
    }

    public void OffQuestListPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnQuest() //���� Ŭ�� �� questcanvas ����
    {
        if (questPrefab == null)
        {
            GameObject Qprefab = Resources.Load<GameObject>("PopUp/Quest_Canvas");
            questPrefab = Instantiate(Qprefab);
        }
        questPrefab.SetActive(true);
    }
}
