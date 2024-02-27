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
    public string[] questName = { "������ óġ", "�ѱ� ����", "�޽��ϱ�", "�ѱ� ����" }; //����Ʈ �߰��� ���� List�� �����ϰ� Add �κп� ���� �Լ� ����°� ���� ��.

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
                questListClear.text = "����";
            }
            else questListClear.text = "�̼���";
        }
        else
        {
            questListClear.text = "Ŭ����";
        }
    }
}
