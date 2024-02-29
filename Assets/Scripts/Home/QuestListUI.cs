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
    //이니셜라이즈 함수를 작성 -> 한번만 작동되도록(첫 세팅) 
    //리프레시 함수 -> 바껴야하는 부분을 전부 작성(짧으면 ㄱㅊ, 길어지면 나눌 것)
    //같은 데이터를 넘겨줄.. 
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
