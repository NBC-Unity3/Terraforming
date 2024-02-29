using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPopupUI : PopupUIBase
{
    public int quest_count;
    public Button[] questTitleButton; //�迭�� �ؼ� QuestPrefab�� �ִ� title�� ����. ������ �ε����� �´� Canvasȭ�� �����ֱ�
    public Button closeButton; //�ڷ� ���� ������ �� �� UI�� ������ ���� �ƴϸ� �ƿ� ��������.

    public Transform questListPosition;
    public QuestListUI[] questList;

    //QuestList��ư�� ������ Quest �������� ���� ���� �����ǵ��� ����. -> Quest ��� ����
    //Quest �������� ��� �ٲ�� �ȵ�. Quest ������ ������ ���� ���ϸ� ���� ���� �ʿ�.
    public GameObject[] questPrefab; // -> Quest ������ ������ �����ִ� Quest_canvas. List�� ���� quest ����'
    public QuestPopupUI[] quests;

    private void Awake()
    {
        quest_count = QuestManager.Instance.quest_count;
        questTitleButton = new Button[quest_count];
        questList = new QuestListUI[quest_count];
        questPrefab = new GameObject[quest_count];
        quests = new QuestPopupUI[quest_count];
    }

    private void Start()
    {
        StartButtonSetting();
        MakeQuestList(quest_count);
    }

    public void MakeQuestList(int index)
    {
        for (int i = 0; i < index; i++)
        {
            questList[i] = PopupUIManager.Instance.OpenPopupUI<QuestListUI>();
            questList[i].transform.parent = questListPosition.transform;

            //�ٸŰ������� ��� ����(�Լ��� �߿�), �ƴϸ� ��ǥ�� ���� �Ѱ��ֱ�(�׳� ���� ���� ���� ��, ���鵵 ���� ���� ��)
            questList[i].GetQuestNumber(i + 1);
            //�����ɸ���, ������ ������
            questList[i].GetquestTitle(QuestManager.Instance.questInstants[i].quest_name);
            questList[i].GetQuestState(QuestManager.Instance.questInstants[i].questState);

            questTitleButton[i] = questList[i].questTitleButton;
            int n = i;
            questTitleButton[i].onClick.AddListener(() => OnQuest(n));
            questTitleButton[i].onClick.AddListener(() => OffQuestListPopup());
        }
    }

    public void OnQuest(int index) //���� Ŭ�� �� questcanvas ����
    {
        if (questPrefab[index] == null)
        {
            quests[index] = PopupUIManager.Instance.OpenPopupUI<QuestPopupUI>();
            quests[index].SetQuestList(questList[index]);
            quests[index].SetQuestInstant(QuestManager.Instance.questInstants[index]);
            quests[index].questCloseButton.onClick.AddListener(() => OnQuestListPopup());
            questPrefab[index] = quests[index].gameObject;
        }
        questPrefab[index].SetActive(true);
    }

    public void StartButtonSetting()
    {
        closeButton.onClick.AddListener(() => OffQuestListPopup());
    }

    public void OffQuestListPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnQuestListPopup()
    {
        gameObject.SetActive(true);
    }
}
