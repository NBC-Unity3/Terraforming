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
    public Action<int> OnGoldValueChange;

    private void Awake()
    {
        Ammo = 1000;
        Gold = 100000000;
        
    }

    private void Start()
    {
    }

    public void Init()
    {
        OnGoldValueChange?.Invoke(Gold);
        OnAmmoValueChange?.Invoke(Ammo);
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
        OnGoldValueChange?.Invoke(Gold);
    }

    public bool UseGold(int amount)
    {
        if(Gold >= amount)
        {
            Gold -= amount;
            OnGoldValueChange?.Invoke(Gold);
            return true;
        }
        else
        {
            return false;
        }
    }
}
