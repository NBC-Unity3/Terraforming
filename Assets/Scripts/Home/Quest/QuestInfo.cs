using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestClearState
{
    NotAccepted, Accepted, Clear, Reward
}

public enum QuestType
{
    Kill, Act
}

public class QuestInfo
{
    //����Ʈ�� ���� ������ ������ ���� == ItemSO

    //�⺻ 4�� ����
    public List<QuestType> questTypes = new List<QuestType> 
    { 
        QuestType.Kill, QuestType.Act, QuestType.Act, QuestType.Act
    };

    public List<string> questNames = new List<string>
    {
        "������ óġ",
        "�ѱ� ����",
        "�޽��ϱ�",
        "�ѱ� ����"
    };
    public List<string> questDescriptions = new List<string>
    {
        "�������� �Ѹ��� óġ",
        "�������� �� �����ϱ�",
        "ȸ�� ��ư�� ������ �޽��ϱ�",
        "������ ������ �����ϱ�"
    };
    public List<int> questGolds = new List<int>{ 2000, 1500, 500, 1000 };

    //Ŭ���� ����� ���� ���� �߰� �ʿ��� ��

    
    public void MakeQuest(QuestType type, string name, string description, int gold)
    {
        questTypes.Add(type);
        questNames.Add(name);
        questDescriptions.Add(description);
        questGolds.Add(gold);
    }
}
