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
    public TMP_Text QuestClear;

    QuestListUI questList;
    //Quest quest;
    public Button questClearButton;
    public Button questCloseButton;


    private void Start()
    {
        SettingQuest();
        questClearButton.onClick.AddListener(() => ChangeQuestText());
        questCloseButton.onClick.AddListener(() => CloseQuest());
    }

    public void SetQuestList(QuestListUI list)
    {
        questList = list;
    }

    public void ChangeQuestText()
    {
        //questList.QuestClearChange(QuestClearState.Accepted); //퀘스트 수락
        //QuestClear.text = questList.questListClear.text;
        //questClearButton.onClick.RemoveListener(() => ChangeQuestText()); //수락하면 버튼 못하도록 설정
        //퀘스트 클리어 시 클리어 부분에 대한 if문 작성 필요
        
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
        //QuestNumber.text = questList.questListNumber.text; //안변할 것
        //QuestTitle.text = questList.questListTitle.text;//안변할 것
        //QuestClear.text = questList.questListClear.text;//버튼 누르면 미수락 -> 수락

        //QuestDescriptionSetting(quest.QuestDiscription(), quest.QuestGold()); //안변할 것
    }

    public void QuestDescriptionSetting(string discription, int gold) //퀘스트에 맞춰 설명이랑 골드 설정
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
