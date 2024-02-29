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
    Quest quest;
    public Button questClearButton;
    public Button questCloseButton;


    private void Start()
    {
        SettingQuest();
        questClearButton.onClick.AddListener(() => ChangeQuestText());

        questCloseButton.onClick.AddListener(() => CloseQuestUI());
        //questCloseButton.onClick.AddListener(() => changeQuest());
    }

    public void SetQuestList(QuestListUI questListUI)
    {
        questList = questListUI;
    }

    public void SetQuestInstant(Quest questInstant)
    {
        quest = questInstant;
    }

    public void ChangeQuestText()
    {
        switch (quest.state)
        {
            case QuestClearState.NotAccepted:
                QuestManager.Instance.AddAcceptedQuest(int.Parse(QuestNumber.text) - 1);
                questList.GetQuestState(quest.state);

                questListState.text = questList.questListState.text;
                break;
            case QuestClearState.Accepted:
                break;
            case QuestClearState.Clear:
                quest.state = QuestClearState.Reward;
                questList.GetQuestState(quest.state);
                questListState.text = questList.questListState.text;
                SettingQuestClear();
                QuestManager.Instance.DicAcceptedQuests.Remove(int.Parse(QuestNumber.text) - 1); //������� �޾����Ƿ� �������� ����Ʈ������ ����
                break;
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
        questListState.text = questList.questListState.text;//��ư ������ �̼��� -> ����

        //QuestDescriptionSetting(quest.quest_description, quest.quest_reward); //�Ⱥ��� ��
    }

    public void QuestDescriptionSetting(string discription, int gold) //����Ʈ�� ���� �����̶� ��� ����
    {
        QuestDescription.text = discription;
        QuestGold.text = gold.ToString();
    }

    //Button
    public void CloseQuestUI()
    {
        gameObject.SetActive(false);
    }
}
