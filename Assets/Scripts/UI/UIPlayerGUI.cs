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

    [Header("Player Stat UI Components")]
    public Image hpBar;
    public Image steminaBar;

    // Start is called before the first frame update
    void Start()
    {
        SetGunInfo(PlayerController.instance.playerShooter.gun);
        ConncetFuncToCurrentGun();
    }

    private void ConncetFuncToCurrentGun()
    {
        PlayerController.instance.playerShooter.onFire += SetCurAmmoText;
    }

    public void SetGunInfo(Gun gun)
    {
        curAmmoText.text = gun.magazine.ToString();
        maxAmmoText.text = gun.capacity.ToString();
    }

    public void SetCurAmmoText(int curAmmo)
    {
        curAmmoText.text = curAmmo.ToString();
    }

    public void OnSwapGun()
    {

    }
}
