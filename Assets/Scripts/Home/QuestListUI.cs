using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListUI : PopupUIBase
{
    //QuestListPopup�� ����� UI
    public Button questTitleButton;

    public TMP_Text questListNumber;
    public TMP_Text questListTitle;
    public TMP_Text questListState;

    public void GetQuestNumber(int n) 
    {
        questListNumber.text = n.ToString();
    }
    //�̴ϼȶ����� �Լ��� �ۼ� -> �ѹ��� �۵��ǵ���(ù ����) 
    //�������� �Լ� -> �ٲ����ϴ� �κ��� ���� �ۼ�(ª���� ����, ������� ���� ��)
    //���� �����͸� �Ѱ���.. 
    public void GetquestTitle(string title)
    {
        questListTitle.text = title;
    }
    public void GetQuestState(QuestClearState clearState)
    {
        switch (clearState)
        {
            case QuestClearState.NotAccepted:
                questListState.text = "���� ����";
                break;
            case QuestClearState.Accepted:
                questListState.text = "����Ʈ ��";
                break;
            case QuestClearState.Clear:
                questListState.text = "Ŭ����";
                break;
            case QuestClearState.Reward:
                questListState.text = "���� ȹ��";
                break;
        }
    }
}
