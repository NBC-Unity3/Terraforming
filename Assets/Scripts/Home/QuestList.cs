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
    public string[] questName = { "슬라임 처치", "총기 구매", "휴식하기", "총기 변경" }; //퀘스트 추가를 위한 List로 변경하고 Add 부분에 대한 함수 만드는게 좋을 듯.

    public bool clear = false;

    private void Start()
    {
        QuestTitleChange();
        QuestClearText(false);
    }

    public void QuestTitleChange()
    {
        int random = Random.Range(0, questName.Length);
        questListTitle.text = questName[random];
    }

    public void QuestClearText(bool check)
    {
        if (!clear)
        {
            if (check)
            {
                questListClear.text = "수락";
            }
            else questListClear.text = "미수락";
        }
        else
        {
            questListClear.text = "클리어";
        }
    }
}
