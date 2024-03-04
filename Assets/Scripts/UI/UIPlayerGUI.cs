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

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.playerShooter.onSwap += SetGunInfo;
        SetGunInfo(PlayerController.instance.playerShooter.gun);
        SetTotalAmmoText(PlayerController.instance.inventory.Ammo);
        ConncetFuncToCurrentGun();
        ConnectFuncToPlayerStat();
    }

    private void ConncetFuncToCurrentGun()
    {
        PlayerController.instance.playerShooter.onFire += SetCurAmmoText;
        PlayerController.instance.inventory.OnAmmoValueChange += SetTotalAmmoText;
    }

    private void ConnectFuncToPlayerStat()
    {
        PlayerController.instance.playerStat.health.OnCurValueChange += SetHpBarFillAmount;
        PlayerController.instance.playerStat.stamina.OnCurValueChange += SetSteminaBarFillAmount;
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
}
