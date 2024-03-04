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
        SetTotalAmmoText();
        ConncetFuncToCurrentGun();
        ConnectFuncToPlayerStat();
    }

    private void ConncetFuncToCurrentGun()
    {
        PlayerController.instance.playerShooter.onFire += SetCurAmmoText;
        PlayerController.instance.playerShooter.onReload += SetTotalAmmoText;
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

    public void SetTotalAmmoText()
    {
        totalAmmoText.text = PlayerController.instance.inventory.Ammo.ToString();
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
