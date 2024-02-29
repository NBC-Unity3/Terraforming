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
    //퀘스트에 대한 정보를 가지고 있음 == ItemSO
    //고유 ID를 이용해서 나중에 dictionary로 관리!
    public QuestType questTypes;
    public int ID; //일반적으로 숫자로 많이 함.
    public string title;
    public string description;
    public int reward; //현재 보상은 골드로 지급하도록.
}
