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
        // �̰� �ȵ�
        //if(target.value == (target.value|1<<collision.gameObject.layer))
        //{
        //    //�÷��̾� ü�� ����

        //    Destroy(gameObject);
        //}
        if(collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾� ü�� ����

        }
        Destroy(gameObject);
    }

}
