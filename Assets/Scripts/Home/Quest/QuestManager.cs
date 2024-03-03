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
    //����Ʈ�� �����ϸ鼭 �ΰ����̶� ��������ִ� ������ ��.
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

    public Object[] questInfoObject; //�����Ǿ����� �ʾƵ� ������ �� �ִ� ����Ʈ�� ���� ������ ������ �ֱ�

    public Quest[] Quests; //�÷��̾ ������ �� �ִ� ����Ʈ ���. ����Ʈ ���� ������ �ֱ� ������ �迭�� �ٲ�.
    //List<QuestInstant> acceptedQuestInstants = new List<QuestInstant>(); //������ ����Ʈ ���.
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
        Quests[index].state = QuestClearState.Accepted; //ChangeQuestState�� �̿��ؼ� �����ϴ� ����� �����غ����� ��.
        DicAcceptedQuests.Add(index, Quests[index]); //����Ʈ�� �� 4���� �ְ� 4�� �߿��� ������ ���� index�� key�� ��Ƽ� ����.
        Debug.Log(DicAcceptedQuests.ContainsKey(index));
    }

    //questState�� clear ���¿��� ���� ȹ�� ��ư�� ������ �Ǹ� ������ ȹ���ϰ� questState�� Reward�� �ǵ��� ����.
    //UI�� ������ �Ǿ����.
    public void GetQuestReward(int index) //
    {
        if (!DicAcceptedQuests.ContainsKey(index)) { return; }//
        Quest quest = new Quest(index);
        DicAcceptedQuests.TryGetValue(index, out quest);
        if(quest.state == QuestClearState.Clear)
        {
            //int gold = quest.questInfo.reward; //���� �ִ� ��忡 �߰��������. int gold�� �ӽ�.
        }
    }

    //���͸� óġ���� �� ų ī��Ʈ �ö� �� �ֵ��� ��������
    public void UpdateQuestKillCount()
    {
        if (DicAcceptedQuests.Count == 0) return; //���� ���� ����Ʈ�� ������ �н�
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
        //������ �̰��� delegate�� event�� �̿��ؾ����� ������ ������ �Ѵ� ����� ���� �ְ�.
        if (Quests[index].kill_count == Quests[index].goal_count)
        {
            //questState�� clear ������ �ع����� clear�� �ʿ���� ������?
            Quests[index].clear = true;
            Quests[index].state = QuestClearState.Clear;
            RemoveQuest(index);
        }
    }

    //�����ʿ�
    public void RemoveQuest(int index)
    {
        DicAcceptedQuests.Remove(index);
        //���� ������ ���� UI ������ ���� �ȵ� �����̹Ƿ� ..
        //Quests[index] = null;
        //MakeQuest(index);
    }


































    //public void MakeQuest(int index)
    //{
    //    //int random = Random.Range(0, questInfo.quest_name.Count);
    //    //Quest quest = new Quest();
    //    //quests[index] = quest;
    //}

    ////������ ����Ʈ�� ���� Type�� �з� �ʿ� -> óġ ���� �ൿ ����
    ////�׷��� ���Ŀ� �ΰ��Ӱ� ���� �� �ϱ� ���� ��
    //public void QuestTypeSort(int index)
    //{
    //    //List ���ؼ� ����� ��쿡�� index�� ã�� ����⶧���� 
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
