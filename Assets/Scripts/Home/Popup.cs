using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{ //����Ǵ� UI �ڵ� ���� �ű⿡ UI �����Ű�� �˾� ������ ����

    //�Ű������� �޴°� ����� ���� ���°��� ���� ���� ��...

    //UI �������� �ε�
    public void OnPopupUI(GameObject selectPopUpUI) //�÷��̾ �ܼ��̶� ��ȣ�ۿ��ϸ� ����
    {
        if (selectPopUpUI == null)
        {
            GameObject canvas = Resources.Load<GameObject>("PopUp/Select_Canvas");
            selectPopUpUI = Instantiate(canvas); //�ѹ� ���� ���Ŀ��� SetActive(false).
        }
        selectPopUpUI.SetActive(true);
    }
}
