using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.VersionControl.ListControl;

public class QuestPopupUI : PopupUIBase
{
    //QuestList�� �մ� title������ �����;���.
    //����κ��� QuestListpopupui.
    int number;

    public TMP_Text QuestNumber;
    public TMP_Text QuestTitle;
    public TMP_Text QuestDescription;
    public TMP_Text QuestGold;
    public TMP_Text questState;

    Quest quest;
    public Button questClearButton;
    public Button questCloseButton;


    private void Start()
    {
        SettingQuest();
        questClearButton.onClick.AddListener(() => ChangeQuestState());

        questCloseButton.onClick.AddListener(() => CloseQuestUI());
    }

    private void OnEnable()
    {
        if (quest == null) return;
        SetQuestStateText(quest.state);
    }

    public void SetQuest(int n, Quest questInfo)
    {
        number = n;
        quest = questInfo;
    }

    public void ChangeQuestState()
    {
        switch (quest.state)
        {
            case QuestClearState.NotAccepted:
                QuestManager.Instance.AddAcceptedQuest(number);
                SetQuestStateText(quest.state);
                break;
            case QuestClearState.Accepted:
                break;
            case QuestClearState.Clear:
                quest.state = QuestClearState.Reward;
                SetQuestStateText(quest.state);

                SettingQuestClear();
                //���� ���� ����Ʈ�� �����ϰ� ���ο� ����Ʈ ��� �ʿ�..
                break;
        }
    }

    public void SettingQuestClear()
    {
        //����Ʈ Ŭ���� �� Ȱ��ȭ��
        QuestManager.Instance.GetQuestReward(quest);
        //��ư ��Ȱ��ȭ �� ��� �����ִ� �κ� ȸ��ǥ��.
    }


    //ó�� ����
    public void SettingQuest()
    {
        //�� ��ȭ�� ��
        QuestNumber.text = (number + 1).ToString();
        QuestTitle.text = quest.questInfoSO.title;
        QuestDescription.text = quest.questInfoSO.description;
        QuestGold.text = quest.questInfoSO.reward.ToString() + "G";

        SetQuestStateText(quest.state);
    }

    public void SetQuestStateText(QuestClearState clearState)
    {
        switch (clearState)
        {
            case QuestClearState.NotAccepted:
                questState.text = "���� ����";
                break;
            case QuestClearState.Accepted:
                questState.text = "����Ʈ ��";
                break;
            case QuestClearState.Clear:
                questState.text = "Ŭ����";
                break;
            case QuestClearState.Reward:
                questState.text = "���� ȹ��";
                break;
        }
    }

    //Button
    public void CloseQuestUI()
    {
        gameObject.SetActive(false);
    }
}
