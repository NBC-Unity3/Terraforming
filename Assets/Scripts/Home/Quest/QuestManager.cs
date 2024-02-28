using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    //����Ʈ�� �����ϸ鼭 �ΰ����̶� ��������ִ� ������ ��.
    public static QuestManager Instance;
    
    public int quest_count = 4;

    QuestInfo questInfo = new QuestInfo(); //�����Ǿ����� �ʾƵ� ������ �� �ִ� ����Ʈ�� ���� ������ ������ �ֱ�

    public QuestInstant[] questInstants; //�÷��̾ ������ �� �ִ� ����Ʈ ���. ����Ʈ ���� ������ �ֱ� ������ �迭�� �ٲ�.
    //List<QuestInstant> acceptedQuestInstants = new List<QuestInstant>(); //������ ����Ʈ ���.
    public Dictionary<int, QuestInstant> DacceptedQuestInstants = new Dictionary<int, QuestInstant>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        questInstants = new QuestInstant[quest_count];
    }

    private void Start()
    {
        MakeQuest();
    }

    //questState�� clear ���¿��� ���� ȹ�� ��ư�� ������ �Ǹ� ������ ȹ���ϰ� questState�� Reward�� �ǵ��� ����.
    //UI�� ������ �Ǿ����.
    public void GetQuestReward(int index) //
    {
        if (!DacceptedQuestInstants.ContainsKey(index)) { return; }//
        QuestInstant questInstant = new QuestInstant();
        DacceptedQuestInstants.TryGetValue(index, out questInstant);
        if(questInstant.questState == QuestClearState.Clear)
        {
            int gold = questInstant.quest_reward; //���� �ִ� ��忡 �߰��������. int gold�� �ӽ�.
        }
    }

    //���͸� óġ���� �� ų ī��Ʈ �ö� �� �ֵ��� ��������
    public void UpdateQuestKillCount()
    {
        if (DacceptedQuestInstants.Count == 0) return; //���� ���� ����Ʈ�� ������ �н�
        for(int i = 0; i < DacceptedQuestInstants.Count; i++)
        {
            if (DacceptedQuestInstants[i].quest_ID.Contains("Kill")) //���� ���� ����Ʈ �߿��� Kill �� ������ 
            {
                DacceptedQuestInstants[i].kill_count++;
                CheckQuestGoal(DacceptedQuestInstants[i]);
                //���Ŀ� Ư�� ���� ��� ���� tag�� ���� �ٲ� �� �ֵ��� ������ �� ����.
            }
        }
    }

    public void CheckQuestGoal(QuestInstant questInstant)
    {
        //������ �̰��� delegate�� event�� �̿��ؾ����� ������ ������ �Ѵ� ����� ���� �ְ�.
        if(questInstant.kill_count == questInstant.quest_goal.monster_count)
        {
            //questState�� clear ������ �ع����� clear�� �ʿ���� ������?
            questInstant.clear = true;
            questInstant.questState = QuestClearState.Clear;
        }
    }

    public void ChangeQuestState()
    {
        //delegate�� event�̿��ؼ� �������� �ٲ�� �����ؾ��� �� ����.. ���� �����غ���
    }

    //����Ʈ ���� â���� ����Ʈ ���� �� ������ ������ ���� ���� Quest ������� �� �� �ְ� ����������.
    //�̰� UI�� ����Ǿ���ϴ� �κ�  
    public void AddAcceptedQuest(int index)
    {
        Debug.Log(DacceptedQuestInstants.ContainsKey(index));
        questInstants[index].questState = QuestClearState.Accepted; //ChangeQuestState�� �̿��ؼ� �����ϴ� ����� �����غ����� ��.
        DacceptedQuestInstants.Add(index, questInstants[index]); //����Ʈ�� �� 4���� �ְ� 4�� �߿��� ������ ���� index�� key�� ��Ƽ� ����.
        Debug.Log(DacceptedQuestInstants.ContainsKey(index));
    }

    public void MakeQuest()
    {
        for (int i = 0; i < quest_count; i++)
        {
            QuestInstant instant = new QuestInstant();
            questInstants[i] = instant;
        }
    }

    public void RemoveQuest()
    {
        for(int i = 0; i < questInstants.Length; i++)
        {
            if (questInstants[i].questState == QuestClearState.Reward) //����Ʈ�� ������ ���� �����̸�..
            {
                //null ���� �����ϸ� ���� �߻��ϱ� ������ ���ο� quest �������� ��ü
                questInstants[i] = new QuestInstant();
            }
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
