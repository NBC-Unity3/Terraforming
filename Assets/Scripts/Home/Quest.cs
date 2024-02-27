using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : PopupUIBase
{
    //QuestList에 잇는 title내용을 가져와야함.
    //연결부분은 QuestListpopupui.
    public TMP_Text QuestNumber;
    public TMP_Text QuestTitle;
    public TMP_Text QuestDescription;
    public TMP_Text QuestGold;
    public TMP_Text QuestClear;

    QuestList questList;
    public Button questClearButton;
    public Button questCloseButton;


    public string[] questName = { "슬라임 처치", "총기 구매", "휴식하기", "총기 변경" }; //QuestList에 있는 배열을 가져오는 것이 좋을 것 같음.
    public string[] questDescription = { 
        "슬라임을 한마리 처치", 
        "상점에서 총 구매하기", 
        "회복 버튼을 눌러서 휴식하기", 
        "구매한 총으로 변경하기" };
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
        questClearButton.onClick.RemoveListener(() => ChangeQuestText()); //수락하면 버튼 못하도록 설정
        //퀘스트 클리어 시 클리어 부분에 대한 if문 작성 필요
        if(questList.clear)
        {
            questClearButton.onClick.AddListener(()=>SettingQuestClear());
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
        QuestClear.text = questList.questListClear.text;//버튼 누르면 미수락 -> 수락

        QuestDescriptionSetting(QuestTitle.text); //안변할 것
    }

    public void QuestDescriptionSetting(string name) //퀘스트에 맞춰 설명이랑 골드 설정
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
