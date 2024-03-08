using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolerView : CoolerElement
{
    public CoolerAnimation coolerAnimation;

    void Start()
    {
        //Add buttons to the cooler UI for each fish held in it
        foreach(FishSO fish in cooler.model.fishInCooler.Keys)
        {
            Button newButton = Instantiate(cooler.model.button,gameObject.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + cooler.model.fishInCooler[fish];
            newButton.onClick.AddListener(delegate {cooler.controller.SpawnFish(fish,newButton); });
            newButton.onClick.AddListener(delegate {coolerAnimation.Slide();});
        }
    }
}
