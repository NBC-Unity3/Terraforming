using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListUI : PopupUIBase
{
    //QuestListPopup에 생기는 UI
    public Button titleButton;

    public TMP_Text listNumber;
    public TMP_Text listTitle;
    public TMP_Text listState;

    public Quest quest;

    private void OnEnable()
    {
        if (quest == null) return;
        SetQuestState(quest.state);
    }

    public void SetQuestList(int n, Quest quest)
    {
        listNumber.text = (n + 1).ToString();
        this.quest = quest;
        listTitle.text = quest.questInfoSO.title;
        SetQuestState(quest.state);
    }

    public void SetQuestState(QuestClearState clearState)
    {
        switch (clearState)
        {
            case QuestClearState.NotAccepted:
                listState.text = "수락 가능";
                break;
            case QuestClearState.Accepted:
                listState.text = "퀘스트 중";
                break;
            case QuestClearState.Clear:
                listState.text = "클리어";
                break;
            case QuestClearState.Reward:
                listState.text = "보상 획득";
                break;
        }
    }
}
