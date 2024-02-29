using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //����Ʈ�� �����ϸ鼭 �ΰ����̶� ��������ִ� ������ ��.
    public static QuestManager Instance;
    
    public int quest_count = 4;

    public Object[] questInfo; //�����Ǿ����� �ʾƵ� ������ �� �ִ� ����Ʈ�� ���� ������ ������ �ֱ�

    public Quest[] Quests; //�÷��̾ ������ �� �ִ� ����Ʈ ���. ����Ʈ ���� ������ �ֱ� ������ �迭�� �ٲ�.
    //List<QuestInstant> acceptedQuestInstants = new List<QuestInstant>(); //������ ����Ʈ ���.
    public Dictionary<int, Quest> DicAcceptedQuests = new Dictionary<int, Quest>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        Quests = new Quest[quest_count];
        questInfo = Resources.LoadAll("QuestSO"); //questInfo�� ���� Ÿ���� ���߿� Ȯ��.
    }

    private void Start()
    {
        MakeQuest();
    }

    public void MakeQuest()
    {
        for (int i = 0; i < quest_count; i++)
        {
            int index = Random.Range(0, questInfo.Length);
            Quest quest = new Quest(index); //i ����
            Quests[i] = quest;
        }
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
        for(int i = 0; i < DicAcceptedQuests.Count; i++)
        {

        }
    }

    public void CheckQuestGoal(Quest quest)
    {
        //������ �̰��� delegate�� event�� �̿��ؾ����� ������ ������ �Ѵ� ����� ���� �ְ�.
        if(quest.kill_count == quest.goal_count)
        {
            //questState�� clear ������ �ع����� clear�� �ʿ���� ������?
            quest.clear = true;
            quest.state = QuestClearState.Clear;
        }
    }

    public void ChangeQuestState()
    {
        //delegate�� event�̿��ؼ� �������� �ٲ�� �����ؾ��� �� ����.. ���� �����غ���
    }

    //����Ʈ ���� â���� ����Ʈ ���� �� ������ ������ ���� ���� Quest ������� �� �� �ְ� ����������.
    //�̰� UI�� ����Ǿ���ϴ� �κ�  
    

    


    //�����ʿ�
    public void RemoveQuest(int index)
    {
        if (Quests[index].state == QuestClearState.Reward) //����Ʈ�� ������ ���� �����̸�..
        {
            //null ���� �����ϸ� ���� �߻��ϱ� ������ ���ο� quest �������� ��ü
            Quests[index] = new Quest(index); //index ����
        }
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
