using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPopupUI : PopupUIBase
{
    public int quest_count = 4;
    public Button[] questTitleButton; //�迭�� �ؼ� QuestPrefab�� �ִ� title�� ����. ������ �ε����� �´� Canvasȭ�� �����ֱ�
    public Button closeButton; //�ڷ� ���� ������ �� �� UI�� ������ ���� �ƴϸ� �ƿ� ��������.

    public Transform questListPosition;
    public QuestList[] questList;

    //QuestList��ư�� ������ Quest �������� ���� ���� �����ǵ��� ����. -> Quest ��� ����
    //Quest �������� ��� �ٲ�� �ȵ�. Quest ������ ������ ���� ���ϸ� ���� ���� �ʿ�.
    public GameObject[] questPrefab; // -> Quest ������ ������ �����ִ� Quest_canvas. List�� ���� quest ����

    private void Awake()
    {
        questTitleButton = new Button[quest_count];
        questList = new QuestList[quest_count];
        questPrefab = new GameObject[quest_count];
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
            questList[i] = PopupUIManager.Instance.OpenPopupUI<QuestList>();
            questList[i].transform.parent = questListPosition.transform;
            questTitleButton[i] = questList[i].questTitleButton;
            int n = i;
            questTitleButton[i].onClick.AddListener(() => OnQuest(n));
            questTitleButton[i].onClick.AddListener(() => OffQuestListPopup());
        }
    }

    public void StartButtonSetting()
    {
        closeButton.onClick.AddListener(() => OffQuestListPopup());
    }

    public void OffQuestListPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnQuest(int index) //���� Ŭ�� �� questcanvas ����
    {
        if (questPrefab[index] == null)
        {
            questPrefab[index] = PopupUIManager.Instance.OpenPopupUI<Quest>().gameObject;
        }
        questPrefab[index].SetActive(true);
    }
}
