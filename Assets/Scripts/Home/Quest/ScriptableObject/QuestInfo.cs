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
    //퀘스트에 대한 정보를 가지고 있음 == ItemSO
    
    public QuestType questTypes;
    public string quest_ID; //고유 ID를 이용해서 나중에 dictionary로 관리!
    public string quest_name;
    public string quest_description;
    public int quest_reward; //현재 보상은 골드로 지급하도록.
    public QuestClearState questState;
}
