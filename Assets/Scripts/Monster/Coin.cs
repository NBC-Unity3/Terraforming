using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어 골드 증가
            collision.gameObject.GetComponent<PlayerInventory>().AddGold(500);
            Destroy(gameObject);
        }

    }
}
