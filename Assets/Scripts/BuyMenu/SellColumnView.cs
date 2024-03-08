using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellColumnView : BuyMenuElement
{

    [SerializeField] TextMeshProUGUI salePriceText;
    [SerializeField] TextMeshProUGUI filetText;
    [SerializeField] TextMeshProUGUI netText;
    
    void Start()
    {
        //Update UI text fields with new information
        filetText.text = "Filets Per Fish: " + buyMenu.model.fish.numOfFilets.ToString();
        UpdateSalePriceText();
        UpdateNetText();
    }


    public void UpdateSalePriceText()
    {
        salePriceText.text = "Price Per Filet: " + buyMenu.model.fish.salePricePerFilet.ToString();
    }

    public void UpdateNetText()
    {
        netText.text = "Net Per Fish: " + (buyMenu.model.fish.salePricePerFilet*buyMenu.model.fish.numOfFilets).ToString();
    }
}
