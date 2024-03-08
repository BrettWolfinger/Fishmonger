using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*Script to manage the behaviors of UI elements on the BuyScreen
*/
public class BuyMenuUI : MonoBehaviour
{

    //Serialized fields for UI components related to an individual fish listing on the buy screen
    [SerializeField] FishSO fish;
    [SerializeField] TextMeshProUGUI FishNameText;
    [SerializeField] TextMeshProUGUI QuantityText;
    [SerializeField] TextMeshProUGUI StockText;
    [SerializeField] TextMeshProUGUI PriceText;
    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI salePriceText;
    [SerializeField] TextMeshProUGUI filetText;
    [SerializeField] TextMeshProUGUI netText;
    
    TextMeshProUGUI buyButtonText;
    CoolerManager coolerManager;
    MoneyManager moneyManager;
    SupplyManager supplyManager;
    int quantity = 1;
    int stock;
    int price;

    //takes the type of fish, the quantity of fish, and the per fish price
    public static Action<FishSO,int,int> FishWasPurchased = delegate { };

    void Awake()
    {
        supplyManager = FindObjectOfType<SupplyManager>();
        coolerManager = FindObjectOfType<CoolerManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        //Get updated stock and price info after economic simulations
        stock = supplyManager.GetFishSupplyData(fish.name).stock;
        price = supplyManager.GetFishSupplyData(fish.name).pricePerFish;

        //Update UI text fields with new information
        FishNameText.text = fish.name;
        UpdateStockText();
        PriceText.text = "Price: $" + price.ToString();
        filetText.text = "Filets Per Fish: " + fish.GetNumFilet().ToString();
        UpdateBuyButton();
        UpdateSalePriceText();
        UpdateNetText();
    }

    //Method called by OnClick of + button for increasing quantity of fish in a single purchase
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

    //Method called by OnClick of - button for decreasing quantity of fish in a single purchase
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

    //straightforward updater methods to set the text on ui components
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

    private void UpdateSalePriceText()
    {
        salePriceText.text = "Price Per Filet: " + fish.GetSalePrice().ToString();
    }

    private void UpdateNetText()
    {
        netText.text = "Net Per Fish: " + (fish.GetSalePrice()*fish.GetNumFilet()).ToString();
    }


    //OnClick method called by the buy button. Calls everything else relevant to signaling
    // a purchase was made.
    public void BuyFish()
    {
        //Allow purchase to happen if the player has enough money for it
        if(moneyManager.GetMoneyTotal() >= quantity*price)
        {
            FishWasPurchased.Invoke(fish,quantity,price);
            //TODO: change these to work with the fish was purchased event 
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
