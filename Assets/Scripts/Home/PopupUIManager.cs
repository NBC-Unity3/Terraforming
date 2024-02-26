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

    private static PopupUIManager instance;
    public static PopupUIManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PopupUIManager();
            }
            return instance;
        }
    }

    public T OpenPopupUI<T>() where T : PopupUIBase
    {
        return OpenPopupUI(typeof(T).Name) as T; //script�̸� == resources�̸�
    }

    public PopupUIBase OpenPopupUI(string name)
    {
        var obj = Resources.Load("Popup/" + name, typeof(GameObject)) as GameObject;
        if(obj == null) { return null; }
        return MakePopupUI(obj);
    }

    public PopupUIBase MakePopupUI(GameObject prefab)
    {
        var obj = Instantiate(prefab);
        return GetComponentPopupUI(obj);
    }

    public PopupUIBase GetComponentPopupUI(GameObject clone)
    {
        var script = clone.GetComponent<PopupUIBase>();
        return script;
    }
}
