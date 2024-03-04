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

[CreateAssetMenu(fileName = "QuestInfo", menuName ="QuestInfo", order = 0)]
public class QuestInfoSO : ScriptableObject
{
    //����Ʈ�� ���� ������ ������ ���� == ItemSO
    //���� ID�� �̿��ؼ� ���߿� dictionary�� ����!
    public QuestType questTypes;
    public int ID; //�Ϲ������� ���ڷ� ���� ��.
    public string title;
    public string description;
    public int reward; //���� ������ ���� �����ϵ���.
}
