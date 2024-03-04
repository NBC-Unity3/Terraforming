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
    public Button closeButton; //뒤로 가기 눌렀을 때 전 UI가 나오게 할지 아니면 아예 꺼버릴지.

    public Transform questListPosition;
    public QuestListUI[] questList;

    //QuestList버튼을 누르면 Quest 프리팹이 일정 갯수 생성되도록 설정. -> Quest 목록 생성
    //Quest 프리팹의 경우 바뀌면 안됨. Quest 갯수가 정해진 갯수 이하면 새로 생성 필요.
    public GameObject[] questPrefab; // -> Quest 제목을 누르면 보여주는 Quest_canvas. List로 만들어서 quest 관리'
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

        //☆매개변수가 길면 따로(함수명 중요), 아니면 쉼표로 같이 넘겨주기(그냥 내가 보기 힘들 때, 남들도 보기 힘들 때)
        questList[index].SetQuestList(index, QuestManager.Instance.Quests[index]);

        int n = index;
        questList[index].titleButton.onClick.AddListener(() => OnQuest(n));
    }

    public void OnQuest(int index) //제목 클릭 시 questcanvas 생성
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

    //퀘스트 보상 획득 시 원래 퀘스트 삭제 및 새로운 퀘스트 생성
    public void MakeNewQuestList(int index)
    {
        QuestManager.Instance.RemoveQuest(index);
        MakeQuestList(index);
        Destroy(questPrefab[index]);
        quests[index] = null;
    }



    //버튼---------------------
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
