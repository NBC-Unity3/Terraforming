using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListUI : PopupUIBase
{
    //QuestListPopup�� ����� UI
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
                listState.text = "���� ����";
                break;
            case QuestClearState.Accepted:
                listState.text = "����Ʈ ��";
                break;
            case QuestClearState.Clear:
                listState.text = "Ŭ����";
                break;
            case QuestClearState.Reward:
                listState.text = "���� ȹ��";
                break;
        }
    }
}
