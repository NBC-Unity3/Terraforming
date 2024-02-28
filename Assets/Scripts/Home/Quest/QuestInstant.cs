using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstant : MonoBehaviour
{
    QuestInfo questInfo = new QuestInfo(); //SO로 한다면 '여기다가 List'해서 랜덤으로 가져오기. Info에 List로 저장한다면 랜덤으로 가져오기만 함.

    public string quest_ID;
    public string quest_name;
    public string quest_description; //QuestGoal에 넣어야하나..
    public QuestGoal quest_goal;
    public int quest_reward;
    public QuestClearState questState;
    public bool clear;
    public int kill_count;

    public QuestInstant()
    {
        quest_ID = questInfo.quest_ID;
        quest_name = questInfo.quest_name;
        quest_description = questInfo.quest_description;
        quest_goal = questInfo.quest_goal;
        quest_reward = questInfo.quest_reward;
        questState = questInfo.questState;
        clear = questInfo.clear;
        kill_count = questInfo.kill_count;
    }
}
