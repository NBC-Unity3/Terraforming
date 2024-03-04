using System;
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

    public Action<int> OnAmmoValueChange;

    private void Awake()
    {
        Ammo = 1000;
        Gold = 100000000;
        
    }

    public void AddAmmo(int amount)
    {
        Ammo += amount;
        OnAmmoValueChange?.Invoke(Ammo);
    }

    public int UseAmmo(int amount)
    {
        if(Ammo >= amount)
        {
            Ammo -= amount;
            OnAmmoValueChange?.Invoke(Ammo);
            return amount;
        }
        else
        {
            int remainAmmo = Ammo;
            OnAmmoValueChange?.Invoke(Ammo);
            Ammo = 0;
            return remainAmmo;
        }
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        Debug.Log(Gold);
    }
}
