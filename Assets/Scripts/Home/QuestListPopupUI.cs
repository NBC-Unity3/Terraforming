using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPopupUI : PopupUIBase
{
    public Button[] questTitleButton = new Button[4]; //배열로 해서 QuestPrefab에 있는 title로 연결. 누르면 인덱스에 맞는 Canvas화면 보여주기
    public Button closeButton; //뒤로 가기 눌렀을 때 전 UI가 나오게 할지 아니면 아예 꺼버릴지.

    public Transform questListPosition;
    public QuestList[] questList = new QuestList[4];

    //QuestList버튼을 누르면 Quest 프리팹이 일정 갯수 생성되도록 설정. -> Quest 목록 생성
    //Quest 프리팹의 경우 바뀌면 안됨. Quest 갯수가 정해진 갯수 이하면 새로 생성 필요.
    public GameObject questPrefab; // -> Quest 제목을 누르면 보여주는 Quest_canvas. List로 만들어서 quest 관리

    private void Start()
    {
        StartButtonSetting();
        for (int i = 0; i < 4; i++)
        {
            questList[i] = PopupUIManager.Instance.OpenPopupUI<QuestList>();
            questList[i].transform.parent = questListPosition.transform;
        }
    }

    public void StartButtonSetting()
    {
        //questTitleButton.onClick.AddListener(() => OnQuest());

        closeButton.onClick.AddListener(() => OffQuestListPopup());
    }

    public void OffQuestListPopup()
    {
        gameObject.SetActive(false);
    }

    public void OnQuest() //제목 클릭 시 questcanvas 생성
    {
        if (questPrefab == null)
        {
            GameObject Qprefab = Resources.Load<GameObject>("PopUp/Quest_Canvas");
            questPrefab = Instantiate(Qprefab);
        }
        questPrefab.SetActive(true);
    }
}
