using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    //questID에 따라 목표가 바껴야함.

    public readonly int monster_count; //처치하는 적의 갯수. 이후에 변하면 안됨.

    public QuestGoal(string id)
    {
        //id에 kill이 포함되어있고 slime이 있으면 슬라임을 랜덤으로 생성된 숫자만큼 처치하면 클리어될 수 있도록 설정해줘야함. ->현재 그냥 모든 몬스터에 관해서 처리중
        if (id.Contains("Kill"))
        {
            monster_count = 1; //이후에 random으로 생성.
        }
    }
}
