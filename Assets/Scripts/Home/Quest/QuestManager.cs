using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //퀘스트를 관리하면서 인게임이랑 연결시켜주는 역할을 함.

    QuestInfo questInfo = new QuestInfo(); //앞으로 생성될 수 있는 퀘스트에 대한 정보.

    public int quest_count = 4;
    public Quest[] quests; //생성된 퀘스트 저장
    public int[] kill_count;
    public bool[] check_act;

    private void Awake()
    {
        quests = new Quest[quest_count];
        kill_count = new int[quest_count];
        check_act = new bool[quest_count];
    }

    private void Start()
    {
        for (int i = 0; i < quest_count; i++)
        {
            MakeQuest(i);
            QuestTypeSort(i);
        }
    }

    public void MakeQuest(int index)
    {
        int random = Random.Range(0, questInfo.questNames.Count);
        Quest quest = new Quest(random);
        quests[index] = quest;
    }

    //생성된 퀘스트에 대한 Type별 분류 필요 -> 처치 따로 행동 따로
    //그래야 추후에 인게임과 연결 시 하기 편할 듯
    public void QuestTypeSort(int index)
    {
        //List 통해서 만드는 경우에는 index로 찾기 힘들기때문에 
        if(quests[index].questType == QuestType.Kill)
        {
            kill_count[index] = 0;
        }
        else if (quests[index].questType == QuestType.Act)
        {
            check_act[index] = false;
        }
    }


    //kill_count랑 check_act 초기화
    public void StartSetting()
    {
        for(int i = 0; i < quest_count; i++)
        {
            kill_count[i] = -1;
            check_act[i] = false;
        }
    }


    //public string QuestState(QuestClearState clearState)
    //{
    //    string stateString = "";
    //    switch (clearState)
    //    {
    //        case QuestClearState.NotAccepted:
    //            stateString = "수락 가능";
    //            break;
    //        case QuestClearState.Accepted:
    //            stateString = "퀘스트 중";
    //            break;
    //        case QuestClearState.Clear:
    //            stateString = "클리어";
    //            break;
    //        case QuestClearState.Reward:
    //            stateString = "보상 획득";
    //            break;
    //    }
    //    return stateString;
    //}
}
