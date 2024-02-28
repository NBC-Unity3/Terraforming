using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //Quest 정보를 가져오는 역할
    QuestInfo questInfo = new QuestInfo();

    public QuestType questType;
    public string questName;
    public string questDescrition;
    public int questGold;
    public QuestClearState questState;

    public Quest(int index)
    {
        questType = questInfo.questTypes[index];
        questName = questInfo.questNames[index];
        questDescrition = questInfo.questDescriptions[index];
        questGold = questInfo.questGolds[index];
        questState = QuestClearState.NotAccepted; //생성 시 수락하기 전 상태
    }
}
