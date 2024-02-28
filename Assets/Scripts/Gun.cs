using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    public string name {get;}
    public float damage {get;}       // 총의 데미지
    public float range {get;}        // 사거리
    public float rpm {get;}          // 연사속도
    public float mag {get;}          // 탄창 용량
    public float recoil {get;}       // 반동
    public float reload {get;}       // 재장전 시간
}
