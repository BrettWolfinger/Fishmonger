using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuUIController : BuyMenuElement
{
    //takes the type of fish, the quantity of fish, and the per fish price
    public static Action<FishSO,int,int> FishWasPurchased = delegate { };
    
    //Method called by OnClick of + button for increasing quantity of fish in a single purchase
    public void IncreaseQuantity()
    {
        //Dont increase quantity past stock
        if(buyMenu.model.quantity < buyMenu.model.stock)
        {
            buyMenu.model.quantity++;
            buyMenu.view.UpdateQuantityText();
            buyMenu.view.UpdateBuyButton();
        }
    }

    //Method called by OnClick of - button for decreasing quantity of fish in a single purchase
    public void DecreaseQuantity()
    {
        //Dont decrease stock past 1;
        if(buyMenu.model.quantity > 1)
        {
            buyMenu.model.quantity--;
            buyMenu.view.UpdateQuantityText();
            buyMenu.view.UpdateBuyButton();
        }
    }
    
    //OnClick method called by the buy button. Calls everything else relevant to signaling
    // a purchase was made.
    public void BuyFish()
    {
        //Allow purchase to happen if the player has enough money for it
        if(buyMenu.model.moneyManager.GetMoneyTotal() >= buyMenu.model.quantity*buyMenu.model.price)
        {
            FishWasPurchased.Invoke(buyMenu.model.fish,buyMenu.model.quantity,buyMenu.model.price);
            //TODO: change these to work with the fish was purchased event 
            buyMenu.model.moneyManager.SubtractMoney(buyMenu.model.quantity*buyMenu.model.price);
            buyMenu.model.stock = buyMenu.model.stock - buyMenu.model.quantity;
            if(buyMenu.model.stock > 0) buyMenu.model.quantity = 1;
            else buyMenu.model.quantity = 0;
            buyMenu.view.UpdateQuantityText();
            buyMenu.view.UpdateStockText();
            buyMenu.view.UpdateBuyButton();
        }
        else
        {
            //not enough money!
        }
    }
}
