using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : MonoBehaviour
{
    //Quest ������ �������� ����
    public KillQuestInfo killQuestInfo;

    public QuestType questType;
    public string questName;
    public string questDescrition;
    public int questGold;
    public QuestClearState questState;

    public KillQuest()
    {
        questType = killQuestInfo.questTypes;
        questName = killQuestInfo.quest_name;
        questDescrition = killQuestInfo.quest_description;
        questGold = killQuestInfo.quest_reward;
        questState = QuestClearState.NotAccepted; //���� �� �����ϱ� �� ����
    }
}
