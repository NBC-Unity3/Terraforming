using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    //questID�� ���� ��ǥ�� �ٲ�����.

    public readonly int monster_count; //óġ�ϴ� ���� ����. ���Ŀ� ���ϸ� �ȵ�.

    public QuestGoal(string id)
    {
        //id�� kill�� ���ԵǾ��ְ� slime�� ������ �������� �������� ������ ���ڸ�ŭ óġ�ϸ� Ŭ����� �� �ֵ��� �����������. ->���� �׳� ��� ���Ϳ� ���ؼ� ó����
        if (id.Contains("Kill"))
        {
            monster_count = 1; //���Ŀ� random���� ����.
        }
    }
}
