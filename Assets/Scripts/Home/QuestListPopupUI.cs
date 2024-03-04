using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPopupUI : PopupUIBase
{
    public int quest_count;
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
        questList = new QuestListUI[quest_count];
        questPrefab = new GameObject[quest_count];
        quests = new QuestPopupUI[quest_count];
    }

    private void Start()
    {
        StartButtonSetting();
        for(int i = 0; i < quest_count; i++)
        {
            MakeQuestList(i);
        }
    }

    public void MakeQuestList(int index)
    {
        questList[index] = PopupUIManager.Instance.OpenPopupUI<QuestListUI>();
        questList[index].transform.parent = questListPosition.transform;

        //�ٸŰ������� ��� ����(�Լ��� �߿�), �ƴϸ� ��ǥ�� ���� �Ѱ��ֱ�(�׳� ���� ���� ���� ��, ���鵵 ���� ���� ��)
        questList[index].SetQuestList(index, QuestManager.Instance.Quests[index]);

        int n = index;
        questList[index].titleButton.onClick.AddListener(() => OnQuest(n));
    }

    public void OnQuest(int index) //���� Ŭ�� �� questcanvas ����
    {
        OffQuestListPopup();
        if (questPrefab[index] == null)
        {
            quests[index] = PopupUIManager.Instance.OpenPopupUI<QuestPopupUI>();
            quests[index].SetQuest(index, QuestManager.Instance.Quests[index]);
            quests[index].questCloseButton.onClick.AddListener(() => OnQuestListPopup());

            questPrefab[index] = quests[index].gameObject;
        }
        questPrefab[index].SetActive(true);
    }

    //����Ʈ ���� ȹ�� �� ���� ����Ʈ ���� �� ���ο� ����Ʈ ����
    public void MakeNewQuestList(int index)
    {
        QuestManager.Instance.RemoveQuest(index);
        MakeQuestList(index);
        Destroy(questPrefab[index]);
        quests[index] = null;
    }



    //��ư---------------------
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
