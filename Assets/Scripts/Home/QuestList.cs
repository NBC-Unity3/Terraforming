using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : PopupUIBase
{
    public Button questTitleButton;

    public TMP_Text questListNumber;
    public TMP_Text questListTitle;
    public TMP_Text questListClear;

    //퀘스트 내용 만들기
    public string[] questName = { "슬라임 처치", "총기 구매", "휴식하기", "총기 변경" };

    private void Start()
    {
        QuestTitleChange();
        QuestClearCheck(false);
    }

    public void QuestTitleChange()
    {
        int random = Random.Range(0, questName.Length);
        questListTitle.text = questName[random];
    }

    public void QuestClearCheck(bool check)
    {
        if (check)
        {
            questListClear.text = "수락";
        }
        else questListClear.text = "미수락"; //클리어 시는 어떻게 할지 고민..
    }
}
