using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListUI : PopupUIBase
{
    //QuestListPopup에 생기는 UI
    public Button questTitleButton;

    public TMP_Text questListNumber;
    public TMP_Text questListTitle;
    public TMP_Text questListState;

    public void GetQuestNumber(int n) 
    {
        questListNumber.text = n.ToString();
    }
    public void GetquestTitle(string title)
    {
        questListTitle.text = title;
    }
    public void GetQuestState(QuestClearState clearState)
    {
        switch (clearState)
        {
            case QuestClearState.NotAccepted:
                questListState.text = "수락 가능";
                break;
            case QuestClearState.Accepted:
                questListState.text = "퀘스트 중";
                break;
            case QuestClearState.Clear:
                questListState.text = "클리어";
                break;
            case QuestClearState.Reward:
                questListState.text = "보상 획득";
                break;
        }
    }
}
