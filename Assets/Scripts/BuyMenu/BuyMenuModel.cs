using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuModel : BuyMenuElement
{
    //Serialized fields for UI components related to an individual fish listing on the buy screen
    [SerializeField] public FishSO fish;
    
    public MoneyManager moneyManager;
    SupplyManager supplyManager;
    public int quantity = 1;
    public int stock;
    public int price;

    void Awake()
    {
        supplyManager = FindObjectOfType<SupplyManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }
    void Start()
    {
        //Get updated stock and price info after economic simulations
        stock = supplyManager.GetFishSupplyData(fish.name).stock;
        price = supplyManager.GetFishSupplyData(fish.name).pricePerFish;
    }
}
