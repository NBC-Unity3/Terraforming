using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo : MonoBehaviour
{
    public string quest_ID;
    public string quest_name;
    public string quest_description; //QuestGoal에 넣어야하나..
    public QuestGoal quest_goal;
    public int quest_reward;
    public QuestClearState questState;
    public bool clear;
    public int kill_count;

    public QuestInfo()
    {
        quest_ID = "Kill_Monster";
        quest_name = "몬스터 처치";
        quest_description = "몬스터를 처치하세요.";
        quest_goal = new QuestGoal(quest_ID);
        quest_reward = 1000;
        questState = QuestClearState.NotAccepted; //첫 생성이므로 항상 NotAccepted 상태
        clear = false; //클리어 하지 않은 상태이므로 항상 false로 생성
        kill_count = 0; //몬스터 처치 횟수를 체크함.
    }
}
