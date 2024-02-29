using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopupUI : PopupUIBase
{
    //QuestList에 잇는 title내용을 가져와야함.
    //연결부분은 QuestListpopupui.
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

        questCloseButton.onClick.AddListener(() => CloseQuestUI());
        //questCloseButton.onClick.AddListener(() => changeQuest());
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
        switch (quest.questState)
        {
            case QuestClearState.NotAccepted:
                QuestManager.Instance.AddAcceptedQuest(int.Parse(QuestNumber.text) - 1);
                questList.GetQuestState(quest.questState);

                questListState.text = questList.questListState.text;
                break;
            case QuestClearState.Accepted:
                break;
            case QuestClearState.Clear:
                quest.questState = QuestClearState.Reward;
                questList.GetQuestState(quest.questState);
                questListState.text = questList.questListState.text;
                SettingQuestClear();
                QuestManager.Instance.DacceptedQuestInstants.Remove(int.Parse(QuestNumber.text) - 1); //보상까지 받았으므로 진행중인 퀘스트에서는 삭제
                break;
        }
    }

    public void SettingQuestClear()
    {
        //퀘스트 클리어 시 활성화됨
        //퀘스트 클리어로 보상을 누르게 되면 골드가 들어옴.
        //버튼 비활성화 및 골드 보여주는 부분 회색표시.
    }


    //처음 세팅
    public void SettingQuest()
    {
        QuestNumber.text = questList.questListNumber.text; //안변할 것
        QuestTitle.text = questList.questListTitle.text;//안변할 것
        questListState.text = questList.questListState.text;//버튼 누르면 미수락 -> 수락

        QuestDescriptionSetting(quest.quest_description, quest.quest_reward); //안변할 것
    }

    public void QuestDescriptionSetting(string discription, int gold) //퀘스트에 맞춰 설명이랑 골드 설정
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
