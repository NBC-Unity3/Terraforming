using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoreItem
{
    public Gun gun;
    public int price;
    public bool isSold;
}

public class StoreUI : PopupUIBase
{
    [Header("Info UI Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Image weaponImage;
    public TextMeshProUGUI playerGoldText;

    [Header("Ability UI Components")]
    public GameObject abilityContainerGO;
    public TextMeshProUGUI attackValueText;
    public TextMeshProUGUI fireRateValueText;
    public TextMeshProUGUI capacityValueText;

    [Header("Button UI Components")]
    public Button previousBtn;
    public Button nextBtn;
    public Button buyBtn;
    public Button alreadyBuyBtn;
    public Button closeBtn;

    [Header("Buy Ammo UI Components")]
    public Sprite ammoSprite;
    public GameObject buyAmmoContainerGO;
    public TMP_InputField inputAmount;
    public TextMeshProUGUI totalPriceText;

    public StoreItem[] items;
    private int index;

    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = PlayerController.instance.inventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousBtn.onClick.AddListener(OnPreviousBtnClick);
        nextBtn.onClick.AddListener(OnNextBtnClick);
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        buyBtn.onClick.AddListener(OnBuyBtnClick);

        index = 0;

        SetInfo(index);
        SetPlayerGoldText();   
    }

    private void OnEnable()
    {
        SetPlayerGoldText();
    }


    public void SetInfo(int index)
    {
        StoreItem item = items[index];

        if(index == 0)
        {
            buyAmmoContainerGO.SetActive(true);
            abilityContainerGO.SetActive(false);
            weaponImage.sprite = ammoSprite;
            nameText.text = "Bullet";
            SetTotalAmmoPriceText();
        }
        else
        {
            buyAmmoContainerGO.SetActive(false);
            abilityContainerGO.SetActive(true);
            nameText.text = item.gun.name;
            attackValueText.text = item.gun.damage.ToString();
            fireRateValueText.text = item.gun.rpm.ToString();
            capacityValueText.text = item.gun.capacity.ToString();
            weaponImage.sprite = item.gun.image;
        }
        priceText.text = item.price.ToString() + " G";

        SetBuyButton(item.isSold);
    }

    public void SetPlayerGoldText(int playerGold)
    {
        playerGoldText.text = playerGold.ToString();
    }

    public void SetBuyButton(bool isSold)
    {
        buyBtn.gameObject.SetActive(!isSold);
        alreadyBuyBtn.gameObject.SetActive(isSold);
    }

    public void SetPlayerGoldText()
    {
        playerGoldText.text = inventory.Gold.ToString() + " G";
    }

    public void SetTotalAmmoPriceText()
    {
        int totalPrice = CalcTotalAmmoPrice();
        totalPriceText.text = totalPrice.ToString() + " G";
    }

    public int CalcTotalAmmoPrice()
    {
        if(int.TryParse(inputAmount.text, out int amount))
        {
            return amount * items[0].price;
        }
        else
        {
            return 0;
        }
    }

    public void OnPreviousBtnClick()
    {
        index = (items.Length + index - 1) % items.Length;

        SetInfo(index);
    }

    public void OnNextBtnClick()
    {
        index = (items.Length + index + 1) % items.Length;

        SetInfo(index);
    }

    public void OnBuyBtnClick()
    {
        if(index == 0)
        {
            if(inventory.Gold >= CalcTotalAmmoPrice())
            {
                inventory.Gold -= CalcTotalAmmoPrice();
                inventory.AddAmmo(int.Parse(inputAmount.text));
                SetPlayerGoldText();
                inputAmount.text = "0";
            }
        }
        else
        {
            if (inventory.Gold >= items[index].price)
            {
                inventory.Gold -= items[index].price;
                items[index].isSold = true;
                inventory.playerGuns[index].isUnlock = true;
                SetPlayerGoldText();
                SetInfo(index);
            }
            else
            {
                Debug.Log("자금 부족");
            }
        }
    }
}
