using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo : MonoBehaviour
{
    public string quest_ID;
    public string quest_name;
    public string quest_description; //QuestGoal�� �־���ϳ�..
    public QuestGoal quest_goal;
    public int quest_reward;
    public QuestClearState questState;
    public bool clear;
    public int kill_count;

    public QuestInfo()
    {
        quest_ID = "Kill_Monster";
        quest_name = "���� óġ";
        quest_description = "���͸� óġ�ϼ���.";
        quest_goal = new QuestGoal(quest_ID);
        quest_reward = 1000;
        questState = QuestClearState.NotAccepted; //ù �����̹Ƿ� �׻� NotAccepted ����
        clear = false; //Ŭ���� ���� ���� �����̹Ƿ� �׻� false�� ����
        kill_count = 0; //���� óġ Ƚ���� üũ��.
    }
}
