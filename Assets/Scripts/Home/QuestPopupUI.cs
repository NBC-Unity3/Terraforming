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
        //questList.QuestClearChange(QuestClearState.Accepted); //����Ʈ ����
        //QuestClear.text = questList.questListClear.text;
        //questClearButton.onClick.RemoveListener(() => ChangeQuestText()); //�����ϸ� ��ư ���ϵ��� ����
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
        //QuestNumber.text = questList.questListNumber.text; //�Ⱥ��� ��
        //QuestTitle.text = questList.questListTitle.text;//�Ⱥ��� ��
        //QuestClear.text = questList.questListClear.text;//��ư ������ �̼��� -> ����

        //QuestDescriptionSetting(quest.QuestDiscription(), quest.QuestGold()); //�Ⱥ��� ��
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
