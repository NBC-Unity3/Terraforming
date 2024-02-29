using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//data
public class Quest
{
    public QuestInfoSO questInfoSO; //SO�� �Ѵٸ� 1��1 �Ѱ��� ����, Goal�� clear Ȯ���� �߰��ϴ�.
    //-> �������ϱ� Quest�� ����Ʈȭ�ؼ� ������ �ִ� ���� ���! DataManager(����� QuestManager)�� �̰� ���� �ƹ����� ����� �� �ֵ���.
    //-> �����ͷθ� ����

    public readonly int goal_count;

    //�������� �޾ƿ��� �� -> ���� �����ؾ���
    public int kill_count;
    public QuestClearState state;
    public bool clear;

    public Quest(int index) //SO�� �� ��쿡�� index �ʿ����
    {
        questInfoSO = Resources.Load<QuestInfoSO>($"QuestSO/Quest{index}");
        goal_count = QuestGoal(questInfoSO.ID);
        kill_count = 0;
        state = QuestClearState.NotAccepted;
        clear = false;
    }

    //����Ʈ ��ǥ�� ���� ����
    //���� óġ�� ���� ����Ʈ�� �����ϹǷ�, 
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
