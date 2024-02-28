using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //퀘스트를 관리하면서 인게임이랑 연결시켜주는 역할을 함.
    public static QuestManager Instance;
    
    public int quest_count = 4;

    QuestInfo questInfo = new QuestInfo(); //생성되어있지 않아도 생성될 수 있는 퀘스트에 대한 정보를 가지고 있기

    public QuestInstant[] questInstants; //플레이어가 수락할 수 있는 퀘스트 목록. 퀘스트 갯수 정해져 있기 때문에 배열로 바꿈.
    //List<QuestInstant> acceptedQuestInstants = new List<QuestInstant>(); //수락한 퀘스트 목록.
    public Dictionary<int, QuestInstant> DacceptedQuestInstants = new Dictionary<int, QuestInstant>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        questInstants = new QuestInstant[quest_count];
    }

    private void Start()
    {
        MakeQuest();
    }

    //questState가 clear 상태에서 보상 획득 버튼을 누르게 되면 보상을 획득하고 questState가 Reward가 되도록 설정.
    //UI랑 연결이 되어야함.
    public void GetQuestReward(int index) //
    {
        if (!DacceptedQuestInstants.ContainsKey(index)) { return; }//
        QuestInstant questInstant = new QuestInstant();
        DacceptedQuestInstants.TryGetValue(index, out questInstant);
        if(questInstant.questState == QuestClearState.Clear)
        {
            int gold = questInstant.quest_reward; //원래 있던 골드에 추가해줘야함. int gold는 임시.
        }
    }

    //몬스터를 처치했을 때 킬 카운트 올라갈 수 있도록 설정해줌
    public void UpdateQuestKillCount()
    {
        if (DacceptedQuestInstants.Count == 0) return; //수행 중인 퀘스트가 없으면 패스
        for(int i = 0; i < DacceptedQuestInstants.Count; i++)
        {
            if (DacceptedQuestInstants[i].quest_ID.Contains("Kill")) //수행 중인 퀘스트 중에서 Kill 이 있으면 
            {
                DacceptedQuestInstants[i].kill_count++;
                CheckQuestGoal(DacceptedQuestInstants[i]);
                //이후에 특정 몬스터 잡기 등은 tag에 따라 바뀔 수 있도록 수정될 수 있음.
            }
        }
    }

    public void CheckQuestGoal(QuestInstant questInstant)
    {
        //오히려 이것을 delegate와 event를 이용해야하지 않을까 싶은데 둘다 사용할 수도 있고.
        if(questInstant.kill_count == questInstant.quest_goal.monster_count)
        {
            //questState로 clear 설정을 해버리면 clear가 필요없지 않을까?
            questInstant.clear = true;
            questInstant.questState = QuestClearState.Clear;
        }
    }

    public void ChangeQuestState()
    {
        //delegate랑 event이용해서 구독으로 바뀌도록 설정해야할 것 같음.. 추후 생각해보기
    }

    //퀘스트 선택 창에서 퀘스트 선택 후 수락을 누르면 수행 중인 Quest 목록으로 들어갈 수 있게 만들어줘야함.
    //이건 UI랑 연결되어야하는 부분  
    public void AddAcceptedQuest(int index)
    {
        Debug.Log(DacceptedQuestInstants.ContainsKey(index));
        questInstants[index].questState = QuestClearState.Accepted; //ChangeQuestState를 이용해서 변경하는 방법도 생각해봐야할 것.
        DacceptedQuestInstants.Add(index, questInstants[index]); //퀘스트는 총 4개가 있고 4개 중에서 선택한 것의 index를 key로 잡아서 저장.
        Debug.Log(DacceptedQuestInstants.ContainsKey(index));
    }

    public void MakeQuest()
    {
        for (int i = 0; i < quest_count; i++)
        {
            QuestInstant instant = new QuestInstant();
            questInstants[i] = instant;
        }
    }

    public void RemoveQuest()
    {
        for(int i = 0; i < questInstants.Length; i++)
        {
            if (questInstants[i].questState == QuestClearState.Reward) //퀘스트의 보상을 받은 상태이면..
            {
                //null 상태 유지하면 오류 발생하기 때문에 새로운 quest 생성으로 대체
                questInstants[i] = new QuestInstant();
            }
        }
    }
    



























    //public void MakeQuest(int index)
    //{
    //    //int random = Random.Range(0, questInfo.quest_name.Count);
    //    //Quest quest = new Quest();
    //    //quests[index] = quest;
    //}

    ////생성된 퀘스트에 대한 Type별 분류 필요 -> 처치 따로 행동 따로
    ////그래야 추후에 인게임과 연결 시 하기 편할 듯
    //public void QuestTypeSort(int index)
    //{
    //    //List 통해서 만드는 경우에는 index로 찾기 힘들기때문에 
    //    if(quests[index].questType == QuestType.Kill)
    //    {
    //        kill_count[index] = 0;
    //    }
    //    else if (quests[index].questType == QuestType.Act)
    //    {
    //        check_act[index] = false;
    //    }
    //}

    //public void Exam()
    //{
    //    if (quests[1].questState == QuestClearState.Accepted && quests[1].questType == QuestType.Kill)
    //    {
    //        //kill_count[1] =
    //    }
    //    if (quests[1].questState == QuestClearState.Accepted && quests[1].questType == QuestType.Act && check_act[1])
    //    {
    //        quests[1].questState = QuestClearState.Clear;
    //    }
    //}



}
