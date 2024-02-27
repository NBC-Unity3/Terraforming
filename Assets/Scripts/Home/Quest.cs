using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : PopupUIBase
{
    //QuestList�� �մ� title������ �����;���.
    //����κ��� QuestListpopupui.
    public TMP_Text QuestNumber;
    public TMP_Text QuestTitle;
    public TMP_Text QuestDescription;
    public TMP_Text QuestGold;
    public TMP_Text QuestClear;

    QuestList questList;

    public string[] questName = { "������ óġ", "�ѱ� ����", "�޽��ϱ�", "�ѱ� ����" };
    public string[] questDescription = { 
        "�������� �Ѹ��� óġ", 
        "�������� �� �����ϱ�", 
        "ȸ�� ��ư�� ������ �޽��ϱ�", 
        "������ ������ �����ϱ�" };
    public int[] questGold = { 2000, 1500, 500, 1000 };

    private void Start()
    {
        SettingQuest();
    }

    public void GetQuestList(QuestList list)
    {
        questList = list;
    }

    public void ChangeQuestClearText()
    {
        questList.QuestClearCheck(true);
        QuestClear.text = questList.questListClear.text;
    }



    //ó�� ����
    public void SettingQuest()
    {
        QuestNumber.text = questList.questListNumber.text; //�Ⱥ��� ��
        QuestTitle.text = questList.questListTitle.text;//�Ⱥ��� ��
        QuestClear.text = questList.questListClear.text;//��ư ������ �̼��� -> ����, �������¿��� ����Ʈ Ŭ������ ������ ��ư ������

        QuestDescriptionSetting(QuestTitle.text); //�Ⱥ��� ��
    }

    public void QuestDescriptionSetting(string name)
    {
        for(int i = 0; i < questName.Length; i++)
        {
            if (questName[i] == name)
            {
                QuestDescription.text = questDescription[i];
                QuestGold.text = questGold[i].ToString();
                break;
            }
        }
    }
}
