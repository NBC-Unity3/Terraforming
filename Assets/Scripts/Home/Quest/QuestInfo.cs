using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestClearState
{
    NotAccepted, Accepted, Clear, Reward
}

public enum QuestType
{
    Kill, Act
}

public class QuestInfo
{
    //퀘스트에 대한 정보를 가지고 있음 == ItemSO

    //기본 4개 설정
    public List<QuestType> questTypes = new List<QuestType> 
    { 
        QuestType.Kill, QuestType.Act, QuestType.Act, QuestType.Act
    };

    public List<string> questNames = new List<string>
    {
        "슬라임 처치",
        "총기 구매",
        "휴식하기",
        "총기 변경"
    };
    public List<string> questDescriptions = new List<string>
    {
        "슬라임을 한마리 처치",
        "상점에서 총 구매하기",
        "회복 버튼을 눌러서 휴식하기",
        "구매한 총으로 변경하기"
    };
    public List<int> questGolds = new List<int>{ 2000, 1500, 500, 1000 };

    //클리어 방법에 대한 내용 추가 필요할 듯

    
    public void MakeQuest(QuestType type, string name, string description, int gold)
    {
        questTypes.Add(type);
        questNames.Add(name);
        questDescriptions.Add(description);
        questGolds.Add(gold);
    }
}
