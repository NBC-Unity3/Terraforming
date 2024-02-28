using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //����Ʈ�� �����ϸ鼭 �ΰ����̶� ��������ִ� ������ ��.

    QuestInfo questInfo = new QuestInfo(); //������ ������ �� �ִ� ����Ʈ�� ���� ����.

    public int quest_count = 4;
    public Quest[] quests; //������ ����Ʈ ����
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

    //������ ����Ʈ�� ���� Type�� �з� �ʿ� -> óġ ���� �ൿ ����
    //�׷��� ���Ŀ� �ΰ��Ӱ� ���� �� �ϱ� ���� ��
    public void QuestTypeSort(int index)
    {
        //List ���ؼ� ����� ��쿡�� index�� ã�� ����⶧���� 
        if(quests[index].questType == QuestType.Kill)
        {
            kill_count[index] = 0;
        }
        else if (quests[index].questType == QuestType.Act)
        {
            check_act[index] = false;
        }
    }


    //kill_count�� check_act �ʱ�ȭ
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
    //            stateString = "���� ����";
    //            break;
    //        case QuestClearState.Accepted:
    //            stateString = "����Ʈ ��";
    //            break;
    //        case QuestClearState.Clear:
    //            stateString = "Ŭ����";
    //            break;
    //        case QuestClearState.Reward:
    //            stateString = "���� ȹ��";
    //            break;
    //    }
    //    return stateString;
    //}
}
