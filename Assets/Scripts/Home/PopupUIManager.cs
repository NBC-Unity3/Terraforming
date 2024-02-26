using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUIManager : MonoBehaviour
{ //연결되는 UI 코드 보고 거기에 UI 연결시키고 팝업 생성할 예정

    //매개변수로 받는거 말고는 답이 없는가에 대한 고찰 중...
    //제네릭으로 받아서 popupUI 생성만 UI 버튼 내용은 UI마다 script넣어주기

    //UI 프리팹을 로드 -> 제네릭으로 바꿔서 공용으로 사용할 수 있도록 수정

    //public void OnPopupUI(GameObject selectPopUpUI) //플레이어가 콘솔이랑 상호작용하면 등장
    //{
    //    if (selectPopUpUI == null)
    //    {
    //        GameObject canvas = Resources.Load<GameObject>("PopUp/Select_Canvas");
    //        selectPopUpUI = Instantiate(canvas); //한번 생성 이후에는 SetActive(false).
    //    }
    //    selectPopUpUI.SetActive(true);
    //}

    public GameObject OnOpenUI<T>(string name)
    {
        var Obj = Resources.Load("Popup/"+name, typeof(GameObject)) as GameObject;
        return Instantiate(Obj);
    }
}
