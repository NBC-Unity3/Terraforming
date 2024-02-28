using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private static QuestManager instance;
    public static QuestManager Instance 
    { 
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(QuestManager).Name;
                instance = obj.AddComponent<QuestManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        quests = new Quest[quest_count];
        kill_count = Enumerable.Repeat(-1, quest_count).ToArray();
        check_act = Enumerable.Repeat(false, quest_count).ToArray();
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
        //int random = Random.Range(0, questInfo.quest_name.Count);
        //Quest quest = new Quest();
        //quests[index] = quest;
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

    public void Exam()
    {
        if (quests[1].questState == QuestClearState.Accepted && quests[1].questType == QuestType.Kill)
        {
            //kill_count[1] =
        }
        if (quests[1].questState == QuestClearState.Accepted && quests[1].questType == QuestType.Act && check_act[1])
        {
            quests[1].questState = QuestClearState.Clear;
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
