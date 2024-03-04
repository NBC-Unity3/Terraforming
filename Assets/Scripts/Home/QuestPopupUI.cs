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
    //QuestList에 잇는 title내용을 가져와야함.
    //연결부분은 QuestListpopupui.
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
                //보상 받은 퀘스트는 삭제하고 새로운 퀘스트 등록 필요..
                break;
        }
    }

    public void SettingQuestClear()
    {
        //퀘스트 클리어 시 활성화됨
        QuestManager.Instance.GetQuestReward(quest);
        //버튼 비활성화 및 골드 보여주는 부분 회색표시.
    }


    //처음 세팅
    public void SettingQuest()
    {
        //안 변화할 것
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
                questState.text = "수락 가능";
                break;
            case QuestClearState.Accepted:
                questState.text = "퀘스트 중";
                break;
            case QuestClearState.Clear:
                questState.text = "클리어";
                break;
            case QuestClearState.Reward:
                questState.text = "보상 획득";
                break;
        }
    }

    //Button
    public void CloseQuestUI()
    {
        gameObject.SetActive(false);
    }
}
