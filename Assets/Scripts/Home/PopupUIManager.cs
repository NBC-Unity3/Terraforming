using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUIManager : MonoBehaviour
{ //����Ǵ� UI �ڵ� ���� �ű⿡ UI �����Ű�� �˾� ������ ����

    //�Ű������� �޴°� ����� ���� ���°��� ���� ���� ��...
    //���׸����� �޾Ƽ� popupUI ������ UI ��ư ������ UI���� script�־��ֱ�

    //UI �������� �ε� -> ���׸����� �ٲ㼭 �������� ����� �� �ֵ��� ����

    //public void OnPopupUI(GameObject selectPopUpUI) //�÷��̾ �ܼ��̶� ��ȣ�ۿ��ϸ� ����
    //{
    //    if (selectPopUpUI == null)
    //    {
    //        GameObject canvas = Resources.Load<GameObject>("PopUp/Select_Canvas");
    //        selectPopUpUI = Instantiate(canvas); //�ѹ� ���� ���Ŀ��� SetActive(false).
    //    }
    //    selectPopUpUI.SetActive(true);
    //}

    public GameObject OnOpenUI<T>(string name)
    {
        var Obj = Resources.Load("Popup/"+name, typeof(GameObject)) as GameObject;
        return Instantiate(Obj);
    }
}
