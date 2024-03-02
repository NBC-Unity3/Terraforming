using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamagable
{
    [Header("Player Stat Fields")]
    [SerializeField] private int maxHp;
    private int curHp;
    [SerializeField] private int maxStemina;
    private int curStemina;

    public Action<float> OnHpChange;
    public Action<float> OnSteminaChange;
    public Action OnDie;

    private void Awake()
    {
        curHp = maxHp;
        curStemina = maxStemina;
    }

    private void Start()
    {
        OnDie += Die;
    }

    public void AddHp(int value)
    {
        curHp += value;

        if(curHp > maxHp) curHp = maxHp;

        OnHpChange?.Invoke(GetHpBarFillAmount());
    }

    public void SubtractHp(int value)
    {
        curHp -= value;

        if(curHp < 0)
        {
            OnDie?.Invoke();
        }

        OnHpChange?.Invoke(GetHpBarFillAmount());
    }

    public void AddStemina(int value)
    {
        curStemina += value;

        if( curStemina > maxStemina) curStemina = maxStemina;

        OnSteminaChange?.Invoke(GetSteminaBarFillAmount());
    }

    public bool UseStemina(int value)
    {
        if(curStemina > value)
        {
            curStemina -= value;
            OnSteminaChange?.Invoke(GetSteminaBarFillAmount());
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetHpBarFillAmount()
    {
        return (curHp / (float)maxHp);
    }

    public float GetSteminaBarFillAmount()
    {
        return curStemina / (float)curStemina;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        SubtractHp(damageAmount);
    }

    private void Die()
    {
        PlayerController.instance.playerAnimator.SetTrigger("Die");
    }
}
