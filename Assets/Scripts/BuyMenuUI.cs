using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuUI : MonoBehaviour
{
    CoolerManager coolerManager;
    MoneyManager moneyManager;
    [SerializeField] FishSO fish;
    [SerializeField] TextMeshProUGUI FishNameText;
    [SerializeField] TextMeshProUGUI QuantityText;
    [SerializeField] TextMeshProUGUI StockText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Button buyButton;
    TextMeshProUGUI buyButtonText;
    int quantity = 1;
    int stock;
    int price;

    void Awake()
    {
        stock = 5;
        price = fish.GetBuyPrice();
        coolerManager = FindObjectOfType<CoolerManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        FishNameText.text = fish.name;
        UpdateStockText();
        PriceText.text = "Price: $" + price.ToString();
        UpdateBuyButton();
    }

    public void IncreaseQuantity()
    {
        //Dont increase quantity past stock
        if(quantity < stock)
        {
            quantity++;
            UpdateQuantityText();
            UpdateBuyButton();
        }
    }

    public void DecreaseQuantity()
    {
        //Dont decrease stock past 1;
        if(quantity > 1)
        {
            quantity--;
            UpdateQuantityText();
            UpdateBuyButton();
        }
    }

    private void UpdateQuantityText()
    {
        QuantityText.text = quantity.ToString();
    }

    private void UpdateStockText()
    {
        StockText.text = "Stock: " + stock.ToString();
    }

    private void UpdateBuyButton()
    {
        buyButtonText.text = "$" + (price*quantity).ToString() + " - Buy";
    }



    public void BuyFish()
    {
        if(moneyManager.GetMoneyTotal() > quantity*price)
        {
            coolerManager.AddFishToCooler(fish, quantity);
            moneyManager.SubtractMoney(quantity*price);
            stock = stock - quantity;
            if(stock > 0) quantity = 1;
            else quantity = 0;
            UpdateQuantityText();
            UpdateStockText();
            UpdateBuyButton();
        }
        else
        {
            //not enough money!
        }
    }
}
