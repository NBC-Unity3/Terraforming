using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PopupUIManager
{
    //���׸����� �޾Ƽ� popupUI ������ UI ��ư ������ UI���� script�־��ֱ�

    //UI �������� �ε� -> ���׸����� �ٲ㼭 �������� ����� �� �ֵ��� ����
    //��ųʸ� key �� �̿��ؼ� ������ �����ϰ� ������ ���� ���� ��ųʸ��� ����
    public Dictionary<string ,GameObject> popupUI = new Dictionary<string ,GameObject>();

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
        var obj = Object.Instantiate(prefab); //MonoBehaviour������ ���������ϵ���
        return GetComponentPopupUI(obj);
    }

    public PopupUIBase GetComponentPopupUI(GameObject clone)
    {
        var script = clone.GetComponent<PopupUIBase>();
        return script;
    }
}
