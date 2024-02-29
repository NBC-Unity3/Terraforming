using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//data
public class Quest
{
    public QuestInfoSO questInfoSO; //SO로 한다면 1대1 한개씩 놓고, Goal과 clear 확인을 추가하는.
    //-> 여러개니깐 Quest를 리스트화해서 가지고 있는 놈을 사용! DataManager(현재는 QuestManager)에 이거 들어가서 아무때나 사용할 수 있도록.
    //-> 데이터로만 존재

    public readonly int goal_count;

    //서버에서 받아오는 것 -> 따로 존재해야함
    public int kill_count;
    public QuestClearState state;
    public bool clear;

    public Quest(int index) //SO로 할 경우에는 index 필요없음
    {
        questInfoSO = Resources.Load<QuestInfoSO>($"QuestSO/Quest{index}");
        goal_count = QuestGoal(questInfoSO.ID);
        kill_count = 0;
        state = QuestClearState.NotAccepted;
        clear = false;
    }

    //퀘스트 목표에 관한 내용
    //현재 처치에 관한 퀘스트만 존재하므로, 
    public int QuestGoal(int id)
    {
        int count = 0;
        switch (id)
        {
            case 0:
                count = 5;
                break;
            case 1:
                count = 3;
                break;
        }
        return count;
    }
}
