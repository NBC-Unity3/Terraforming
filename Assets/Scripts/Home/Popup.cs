using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{ //����Ǵ� UI �ڵ� ���� �ű⿡ UI �����Ű�� �˾� ������ ����


    //UI �������� �ε�
    public GameObject OnPopupUI(GameObject selectPopUpUI) //�÷��̾ �ܼ��̶� ��ȣ�ۿ��ϸ� ����
    {
        if (selectPopUpUI == null)
        {
            selectPopUpUI = Resources.Load<GameObject>("PopUp_UI/Select_Canvas");
        }
        return selectPopUpUI; //Instantiate�� ���� ���� �ʼ�.
    }


}
