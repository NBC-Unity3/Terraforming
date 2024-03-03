using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //퀘스트를 관리하면서 인게임이랑 연결시켜주는 역할을 함.
    private static QuestManager instance;
    
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(QuestManager).Name;
                instance = obj.AddComponent<QuestManager>();

            }
            return instance;
        }
        
    }

    public int quest_count = 4;

    public Object[] questInfoObject; //생성되어있지 않아도 생성될 수 있는 퀘스트에 대한 정보를 가지고 있기

    public Quest[] Quests; //플레이어가 수락할 수 있는 퀘스트 목록. 퀘스트 갯수 정해져 있기 때문에 배열로 바꿈.
    //List<QuestInstant> acceptedQuestInstants = new List<QuestInstant>(); //수락한 퀘스트 목록.
    public Dictionary<int, Quest> DicAcceptedQuests = new Dictionary<int, Quest>();

    private void Awake()
    {
        Quests = new Quest[quest_count];
        questInfoObject = Resources.LoadAll("QuestSO");
        for (int i = 0; i < quest_count; i++)
        {
            MakeQuest(i);
        }
    }

    public void MakeQuest(int index)
    {
        int random_index = Random.Range(0, questInfoObject.Length);
        Quest quest = new Quest(random_index);
        Quests[index] = quest;
    }

    public void AddAcceptedQuest(int index)
    {
        Debug.Log(DicAcceptedQuests.ContainsKey(index));
        Quests[index].state = QuestClearState.Accepted; //ChangeQuestState를 이용해서 변경하는 방법도 생각해봐야할 것.
        DicAcceptedQuests.Add(index, Quests[index]); //퀘스트는 총 4개가 있고 4개 중에서 선택한 것의 index를 key로 잡아서 저장.
        Debug.Log(DicAcceptedQuests.ContainsKey(index));
    }

    //questState가 clear 상태에서 보상 획득 버튼을 누르게 되면 보상을 획득하고 questState가 Reward가 되도록 설정.
    //UI랑 연결이 되어야함.
    public void GetQuestReward(int index) //
    {
        if (!DicAcceptedQuests.ContainsKey(index)) { return; }//
        Quest quest = new Quest(index);
        DicAcceptedQuests.TryGetValue(index, out quest);
        if(quest.state == QuestClearState.Clear)
        {
            //int gold = quest.questInfo.reward; //원래 있던 골드에 추가해줘야함. int gold는 임시.
        }
    }

    //몬스터를 처치했을 때 킬 카운트 올라갈 수 있도록 설정해줌
    public void UpdateQuestKillCount()
    {
        if (DicAcceptedQuests.Count == 0) return; //수행 중인 퀘스트가 없으면 패스
        for(int i = 0; i < quest_count; i++)
        {
            if (DicAcceptedQuests.ContainsKey(i))
            {
                DicAcceptedQuests[i].kill_count++;
                Debug.Log(DicAcceptedQuests[i].kill_count);
                CheckQuestGoal(i);
            }
        }
    }

    public void CheckQuestGoal(int index)
    {
        //오히려 이것을 delegate와 event를 이용해야하지 않을까 싶은데 둘다 사용할 수도 있고.
        if (Quests[index].kill_count == Quests[index].goal_count)
        {
            //questState로 clear 설정을 해버리면 clear가 필요없지 않을까?
            Quests[index].clear = true;
            Quests[index].state = QuestClearState.Clear;
            RemoveQuest(index);
        }
    }

    //수정필요
    public void RemoveQuest(int index)
    {
        DicAcceptedQuests.Remove(index);
        //새로 생성한 것을 UI 연동이 아직 안된 상태이므로 ..
        //Quests[index] = null;
        //MakeQuest(index);
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
