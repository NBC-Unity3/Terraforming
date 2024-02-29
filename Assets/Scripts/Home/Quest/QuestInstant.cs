using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstant
{
    QuestInfo questInfo = new QuestInfo(); //SO�� �Ѵٸ� 1��1 �Ѱ��� ����, Goal�� clear Ȯ���� �߰��ϴ�.
    //->�������ϱ� QUestInstant�� ����Ʈȭ�ؼ� ������ �ִ� ���� ���! DataManager�� �̰� ���� �ƹ����� ����� �� �ֵ���. -> �����ͷθ� ����

    public string quest_ID;
    public string quest_name;
    public string quest_description; //QuestGoal�� �־���ϳ�..
    public QuestGoal quest_goal;
    public int quest_reward;
    
    public int kill_count;

    //�������� �޾ƿ��� �� -> ���� �����ؾ���
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
