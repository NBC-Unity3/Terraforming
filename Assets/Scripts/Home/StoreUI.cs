using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoreItem
{
    public string name;
    public int price;
    public float attackValue;
    public float fireRateValue;
    public float knockbackValue;
    public Sprite weaponSprite;
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
    public TextMeshProUGUI attackValueText;
    public TextMeshProUGUI fireRateValueText;
    public TextMeshProUGUI knockbackValueText;

    [Header("Button UI Components")]
    public Button previousBtn;
    public Button nextBtn;
    public Button buyBtn;
    public Button alreadyBuyBtn;
    public Button closeBtn;

    public StoreItem[] items;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        previousBtn.onClick.AddListener(OnPreviousBtnClick);
        nextBtn.onClick.AddListener(OnNextBtnClick);
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));

        index = 0;

        SetInfo(index);
    }

    public void SetInfo(int index)
    {
        StoreItem item = items[index];

        nameText.text = item.name;
        priceText.text = item.price.ToString();
        attackValueText.text = item.attackValue.ToString();
        fireRateValueText.text = item.fireRateValue.ToString();
        knockbackValueText.text = item.knockbackValue.ToString();
        weaponImage.sprite = item.weaponSprite;

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
}
