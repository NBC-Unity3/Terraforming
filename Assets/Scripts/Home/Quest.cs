using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Button questClearButton;
    public Button questCloseButton;


    public string[] questName = { "������ óġ", "�ѱ� ����", "�޽��ϱ�", "�ѱ� ����" }; //QuestList�� �ִ� �迭�� �������� ���� ���� �� ����.
    public string[] questDescription = { 
        "�������� �Ѹ��� óġ", 
        "�������� �� �����ϱ�", 
        "ȸ�� ��ư�� ������ �޽��ϱ�", 
        "������ ������ �����ϱ�" };
    public int[] questGold = { 2000, 1500, 500, 1000 };

    private void Start()
    {
        SettingQuest();
        questClearButton.onClick.AddListener(() => ChangeQuestText());
        questCloseButton.onClick.AddListener(() => CloseQuest());
    }

    public void GetQuestList(QuestList list)
    {
        questList = list;
    }

    public void ChangeQuestText()
    {
        questList.QuestClearText(true);
        QuestClear.text = questList.questListClear.text;
        questClearButton.onClick.RemoveListener(() => ChangeQuestText()); //�����ϸ� ��ư ���ϵ��� ����
        //����Ʈ Ŭ���� �� Ŭ���� �κп� ���� if�� �ۼ� �ʿ�
        if(questList.clear)
        {
            questClearButton.onClick.AddListener(()=>SettingQuestClear());
        }
    }

    public void SettingQuestClear()
    {
        //����Ʈ Ŭ���� �� Ȱ��ȭ��
        //����Ʈ Ŭ����� ������ ������ �Ǹ� ��尡 ����.
        //��ư ��Ȱ��ȭ �� ��� �����ִ� �κ� ȸ��ǥ��.
    }


    //ó�� ����
    public void SettingQuest()
    {
        QuestNumber.text = questList.questListNumber.text; //�Ⱥ��� ��
        QuestTitle.text = questList.questListTitle.text;//�Ⱥ��� ��
        QuestClear.text = questList.questListClear.text;//��ư ������ �̼��� -> ����

        QuestDescriptionSetting(QuestTitle.text); //�Ⱥ��� ��
    }

    public void QuestDescriptionSetting(string name) //����Ʈ�� ���� �����̶� ��� ����
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

    //Button
    public void CloseQuest()
    {
        gameObject.SetActive(false);
    }
}
