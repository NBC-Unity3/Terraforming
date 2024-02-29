using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstant
{
    QuestInfo questInfo = new QuestInfo(); //SO로 한다면 1대1 한개씩 놓고, Goal과 clear 확인을 추가하는.
    //->여러개니깐 QUestInstant를 리스트화해서 가지고 있는 놈을 사용! DataManager에 이거 들어가서 아무때나 사용할 수 있도록. -> 데이터로만 존재

    public string quest_ID;
    public string quest_name;
    public string quest_description; //QuestGoal에 넣어야하나..
    public QuestGoal quest_goal;
    public int quest_reward;
    
    public int kill_count;

    //서버에서 받아오는 것 -> 따로 존재해야함
    public QuestClearState questState;
    public bool clear;

    public QuestInstant()
    {
        int index = Random.Range(0, questInfo.quest_ID.Count);
        quest_ID = questInfo.quest_ID[index];
        quest_name = questInfo.quest_name[index];
        quest_description = questInfo.quest_description[index];
        quest_goal = new QuestGoal(quest_ID);
        quest_reward = questInfo.quest_reward[index];
        questState = questInfo.questState;
        clear = questInfo.clear;
        kill_count = questInfo.kill_count;
    }
}
