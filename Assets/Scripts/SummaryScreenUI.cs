using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryScreenUI : MonoBehaviour
{
    [SerializeField] FishSO fish;
    [SerializeField] TextMeshProUGUI fishNameText;
    [SerializeField] TextMeshProUGUI fishSaleDataText;
    [SerializeField] TextMeshProUGUI netText;
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] TextMeshProUGUI profitText;
    [SerializeField] TextMeshProUGUI totalSales;
    List<FiletModel.FiletStruct> list;
    SaleLedger saleLedger;
    PurchaseLedger purchaseLedger;
    SupplyManager supplyManager;

    void Awake()
    {
        saleLedger = FindObjectOfType<SaleLedger>();
        purchaseLedger = FindObjectOfType<PurchaseLedger>();
        supplyManager = FindObjectOfType<SupplyManager>();
    }

    void Start()
    {
        fishNameText.text = fish.name;
        GenerateTestSummary();
    }
    //Since I want to skip the gameplay screen while building economics system, 
    //using this summary for now to bypass the sale ledger.
    private void GenerateTestSummary()
    {
        Dictionary<string,int> dict = purchaseLedger.purchaseLedger;
        int filets = 0;
        int net = 0;
        int input = 0;
        
        if(dict.ContainsKey(fish.name))
        {
            filets = dict[fish.name]*fish.numOfFilets;
            net = filets*fish.salePricePerFilet;
            input = dict[fish.name] * supplyManager.fishSupplyDatabase[fish.name].pricePerFish; 
        }
        
        fishSaleDataText.text = filets + "/" + filets;
        netText.text = "Net $" + net;
        inputText.text = "Input $" + input;
        profitText.text = "Profit $" + (net - input);
        
    }
    
    private void GenerateTrueSummary()
    {
        int net = 0;
        int filets = 0;
        list = saleLedger.list;
        Dictionary<string,int> dict = purchaseLedger.purchaseLedger;
        fishNameText.text = fish.name;
        foreach(FiletModel.FiletStruct sale in list)
        {
            //only process this type of fish
            if(sale.name == fish.name)
            {
                net += sale.salePrice;
                filets += 1;
            }
        }

        int input = dict[fish.name] * supplyManager.fishSupplyDatabase[fish.name].pricePerFish;
        fishSaleDataText.text = filets + "/" + (dict[fish.name]*fish.numOfFilets);
        netText.text = "Net $" + net;
        inputText.text = "Input $" + input;
        profitText.text = "Profit $" + (net - input);
    }
}
