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
        "���� óġ", "������ óġ"
    };
    public List<string> quest_description = new List<string> //QuestGoal�� �־���ϳ�..
    {
        "���͸� óġ�ϼ���.", "�����Ӹ� óġ�ϼ���."
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
