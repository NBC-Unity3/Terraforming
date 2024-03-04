using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerGun
{
    public Gun gun;
    public GameObject gunPrefab;
    public bool isUnlock;
}

public class PlayerInventory : MonoBehaviour
{
    public PlayerGun[] playerGuns;
    public int Ammo {  get;  set; }
    public int Gold { get; set; }

    private void Awake()
    {
        Ammo = 10;
        Gold = 100000000;
        
    }

    public void AddAmmo(int amount)
    {
        Ammo += amount;
        Debug.Log(Ammo);
    }

    public int UseAmmo(int amount)
    {
        if(Ammo >= amount)
        {
            Ammo -= amount;
            return amount;
        }
        else
        {
            int remainAmmo = Ammo;
            Ammo = 0;
            return remainAmmo;
        }
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }
}
