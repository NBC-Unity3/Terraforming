using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour
{
    public LayerMask target;
    public int damage;
    private Rigidbody _rigidbody;
    private void Start()
    {
        Destroy(gameObject,3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 이거 안됨
        //if(target.value == (target.value|1<<collision.gameObject.layer))
        //{
        //    //플레이어 체력 감소

        //    Destroy(gameObject);
        //}
        if(collision.gameObject.CompareTag("Player"))
        {
            //플레이어 체력 감소

        }
        Destroy(gameObject);
    }

}
