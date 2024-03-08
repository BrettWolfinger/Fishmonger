using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*Script to manage the behaviors of UI elements on the BuyScreen
*/
public class BuyMenuView : BuyMenuElement
{

    public SellColumnView sellColumnView;

    //Serialized fields for UI components related to an individual fish listing on the buy screen
    [SerializeField] TextMeshProUGUI FishNameText;
    [SerializeField] TextMeshProUGUI QuantityText;
    [SerializeField] TextMeshProUGUI StockText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Button buyButton;
    
    TextMeshProUGUI buyButtonText;

    void Awake()
    {
        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        //Update UI text fields with new information
        FishNameText.text = buyMenu.model.fish.name;
        UpdateStockText();
        PriceText.text = "Price: $" + buyMenu.model.price.ToString();
        UpdateBuyButton();
    }


    //straightforward updater methods to set the text on ui components
    public void UpdateQuantityText()
    {
        QuantityText.text = buyMenu.model.quantity.ToString();
    }

    public void UpdateStockText()
    {
        StockText.text = "Stock: " + buyMenu.model.stock.ToString();
    }

    public void UpdateBuyButton()
    {
        buyButtonText.text = "$" + (buyMenu.model.price*buyMenu.model.quantity).ToString() + " - Buy";
    }
}
