using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{ //연결되는 UI 코드 보고 거기에 UI 연결시키고 팝업 생성할 예정


    //UI 프리팹을 로드
    public GameObject OnPopupUI(GameObject selectPopUpUI) //플레이어가 콘솔이랑 상호작용하면 등장
    {
        if (selectPopUpUI == null)
        {
            selectPopUpUI = Resources.Load<GameObject>("PopUp_UI/Select_Canvas");
        }
        return selectPopUpUI; //Instantiate를 통해 생성 필수.
    }


}
