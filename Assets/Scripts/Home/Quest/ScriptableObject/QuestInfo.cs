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

[CreateAssetMenu(fileName = "QuestInfo", menuName ="QuestInfo/Default", order = 0)]
public class QuestInfo : ScriptableObject
{
    //����Ʈ�� ���� ������ ������ ���� == ItemSO
    
    public QuestType questTypes;
    public string quest_ID; //���� ID�� �̿��ؼ� ���߿� dictionary�� ����!
    public string quest_name;
    public string quest_description;
    public int quest_reward; //���� ������ ���� �����ϵ���.
    public QuestClearState questState;
}
