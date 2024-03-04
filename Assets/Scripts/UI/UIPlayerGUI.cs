using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerGUI : MonoBehaviour
{
    [Header("Gun Info UI Components")]
    public Image weaponImage;
    public TextMeshProUGUI curAmmoText;
    public TextMeshProUGUI maxAmmoText;
    public TextMeshProUGUI totalAmmoText;

    [Header("Player Stat UI Components")]
    public Image hpBar;
    public Image steminaBar;

    [Header("Player Gold Info UI Components")]
    public TextMeshProUGUI playerGoldText;

    // Start is called before the first frame update
    void Start()
    {
        ConncetFuncToCurrentGun();
        ConnectFuncToPlayerStat();
        ConnectFuncToPlayerInventory();
    }

    private void ConncetFuncToCurrentGun()
    {
        PlayerController.instance.playerShooter.onSwap += SetGunInfo;
        PlayerController.instance.playerShooter.onFire += SetCurAmmoText;
        PlayerController.instance.playerShooter.Init();
    }

    private void ConnectFuncToPlayerStat()
    {
        PlayerController.instance.playerStat.health.OnCurValueChange += SetHpBarFillAmount;
        PlayerController.instance.playerStat.stamina.OnCurValueChange += SetSteminaBarFillAmount;
    }

    private void ConnectFuncToPlayerInventory()
    {
        PlayerController.instance.inventory.OnAmmoValueChange += SetTotalAmmoText;
        PlayerController.instance.inventory.OnGoldValueChange += SetPlayerGoldText;
        PlayerController.instance.inventory.Init();
    }

    public void SetGunInfo(Gun gun)
    {
        weaponImage.sprite = gun.image;
        curAmmoText.text = gun.magazine.ToString();
        maxAmmoText.text = gun.capacity.ToString();
    }

    public void SetCurAmmoText(int curAmmo)
    {
        curAmmoText.text = curAmmo.ToString();
    }

    public void SetTotalAmmoText(int amount)
    {
        totalAmmoText.text = amount.ToString();
    }

    public void SetHpBarFillAmount(float value)
    {
        hpBar.fillAmount = value;
    }

    public void SetSteminaBarFillAmount(float value)
    {
        steminaBar.fillAmount = value;
    }

    public void SetPlayerGoldText(int amount)
    {
        playerGoldText.text = amount.ToString();
    }
}
