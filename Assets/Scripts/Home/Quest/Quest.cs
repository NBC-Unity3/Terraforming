using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    //Quest ������ �������� ����
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
        questState = QuestClearState.NotAccepted; //���� �� �����ϱ� �� ����
    }
}
