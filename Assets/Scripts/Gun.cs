using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    public string name;
    public float damage;        // ���� ������
    public float range;         // ��Ÿ�
    public float rpm;           // ����ӵ�
    public int capacity;        // źâ �뷮
    public int magazine;        // ������ źȯ
    public float recoil;        // �ݵ�
    public float reload;        // ������ �ð�
}