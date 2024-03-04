using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    public string name;
    public float damage;        // 총의 데미지
    public float range;         // 사거리
    public float rpm;           // 연사속도
    public int capacity;        // 탄창 용량
    public int magazine;        // 장전된 탄환
    public float recoil;        // 반동
    public float reload;        // 재장전 시간

    public Sprite image;

    public Vector3 leftHandlePosition;
    public Quaternion leftHandleRotation;
    public Vector3 rightHandlePosition;
    public Quaternion rightHandleRotation;
    public Vector3 firePosition;
}

