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

    //����Ʈ ���� �����
    public string[] questName = { "������ óġ", "�ѱ� ����", "�޽��ϱ�", "�ѱ� ����" };

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
            questListClear.text = "����";
        }
        else questListClear.text = "�̼���"; //Ŭ���� �ô� ��� ���� ���..
    }
}
