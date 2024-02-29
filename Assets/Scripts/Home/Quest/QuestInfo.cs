using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    public List<string> quest_ID = new List<string>
    {
        "Kill_Monster", "Kill_Slime"
    };
    public List<string> quest_name = new List<string>
    {
        "몬스터 처치", "슬라임 처치"
    };
    public List<string> quest_description = new List<string> //QuestGoal에 넣어야하나..
    {
        "몬스터를 처치하세요.", "슬라임를 처치하세요."
    };
    public List<int> quest_reward = new List<int>
    {
        1000, 1200
    };
    public QuestClearState questState = QuestClearState.NotAccepted;
    public bool clear = false;
    public int kill_count = 0;

    public QuestInfo() 
    {
        
    }
}
