using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{ //연결되는 UI 코드 보고 거기에 UI 연결시키고 팝업 생성할 예정

    //매개변수로 받는거 말고는 답이 없는가에 대한 고찰 중...

    //UI 프리팹을 로드
    public void OnPopupUI(GameObject selectPopUpUI) //플레이어가 콘솔이랑 상호작용하면 등장
    {
        if (selectPopUpUI == null)
        {
            GameObject canvas = Resources.Load<GameObject>("PopUp/Select_Canvas");
            selectPopUpUI = Instantiate(canvas); //한번 생성 이후에는 SetActive(false).
        }
        selectPopUpUI.SetActive(true);
    }
}
