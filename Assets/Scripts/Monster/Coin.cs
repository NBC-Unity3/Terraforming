using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾� ��� ����
            PlayerController.instance.inventory.Gold += 1000;
            Destroy(gameObject);
        }

    }
}
