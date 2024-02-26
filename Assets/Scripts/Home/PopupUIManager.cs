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
        return OpenPopupUI(typeof(T).Name) as T; //script이름 == resources이름
    }

    public PopupUIBase OpenPopupUI(string name)
    {
        var obj = Resources.Load("Popups/" + name, typeof(GameObject)) as GameObject;
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
