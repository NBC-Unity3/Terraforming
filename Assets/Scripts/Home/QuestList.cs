using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : PopupUIBase
{
    public Button questTitleButton;

    public TMP_Text questNumber;
    public TMP_Text questTitle; //title Text를 저장하는 곳이 필요함. 그에 따라 Canvas 설정이 바뀜. +여기에 버튼설정해서 이동하도록 하는 것이 나을지도?
    public TMP_Text questClear;

}
