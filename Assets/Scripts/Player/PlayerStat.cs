using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Action<float> OnCurValueChange;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
        OnCurValueChange?.Invoke(GetPercentage());
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
        OnCurValueChange?.Invoke(GetPercentage());
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}

public class PlayerStat : MonoBehaviour, IDamagable
{
    [Header("Player Stat Fields")]
    public Condition health;
    public Condition stamina;

    public Action OnDie;

    private void Start()
    {
        OnDie += Die;
        health.curValue = health.startValue;
        stamina.curValue = stamina.startValue;
        health.Add(0f);
        stamina.Add(0f);
    }

    private void Update()
    {
        stamina.Add(stamina.regenRate * Time.deltaTime);
        if(health.curValue <= 0f)
        {
            OnDie?.Invoke();
            OnDie = null;
        }
    }

    public void AddHp(float value)
    {
        health.Add(value);
    }

    public void SubtractHp(float value)
    {
        health.Subtract(value);
    }

    public bool UseStemina(float value)
    {
        if (stamina.curValue - value < 0)
            return false;

        stamina.Subtract(value);
        return true;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        SubtractHp(damageAmount);
    }

    private void Die()
    {
        Destroy(PlayerController.instance.playerShooter.curGun);
        PlayerController.instance.playerAnimator.SetLayerWeight(1, 0);
        PlayerController.instance.playerAnimator.SetTrigger("Die");
    }
}
