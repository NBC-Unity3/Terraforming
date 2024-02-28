using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopupUI : PopupUIBase
{
    //QuestList�� �մ� title������ �����;���.
    //����κ��� QuestListpopupui.
    public TMP_Text QuestNumber;
    public TMP_Text QuestTitle;
    public TMP_Text QuestDescription;
    public TMP_Text QuestGold;
    public TMP_Text questListState;

    QuestListUI questList;
    QuestInstant quest;
    public Button questClearButton;
    public Button questCloseButton;


    private void Start()
    {
        SettingQuest();
        questClearButton.onClick.AddListener(() => ChangeQuestText());
        questCloseButton.onClick.AddListener(() => CloseQuest());
    }

    public void SetQuestList(QuestListUI questListUI)
    {
        questList = questListUI;
    }

    public void SetQuestInstant(QuestInstant questInstant)
    {
        quest = questInstant;
    }

    public void ChangeQuestText()
    {
        if(quest.questState == QuestClearState.NotAccepted)
        {
            QuestManager.Instance.AddAcceptedQuest(int.Parse(QuestNumber.text) - 1);
            questList.GetQuestState(quest.questState);
            
            questListState.text = questList.questListState.text;
        }
        
        //����Ʈ Ŭ���� �� Ŭ���� �κп� ���� if�� �ۼ� �ʿ�

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
        questListState.text = questList.questListState.text;//��ư ������ �̼��� -> ����

        QuestDescriptionSetting(quest.quest_description, quest.quest_reward); //�Ⱥ��� ��
    }

    public void QuestDescriptionSetting(string discription, int gold) //����Ʈ�� ���� �����̶� ��� ����
    {
        QuestDescription.text = discription;
        QuestGold.text = gold.ToString();
    }

    //Button
    public void CloseQuest()
    {
        gameObject.SetActive(false);
    }
}
