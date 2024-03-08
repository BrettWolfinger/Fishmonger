using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CoolerModel : CoolerElement
{
    [SerializeField] FishSOCollection allFish;
    [SerializeField] public Button button;
    public Dictionary<FishSO,int> fishInCooler;
    string path;
    public PurchaseLedger purchaseLedger;

    void Awake()
    {
        path = Application.persistentDataPath + "/Cooler.json";
        fishInCooler = new Dictionary<FishSO,int>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }
    
    void Load()
    {
        Dictionary<string,int> dict = purchaseLedger.purchaseLedger;

        //convert strings fishnames into FishSO for spawning
        foreach(string fishName in dict.Keys)
        {
            FishSO result = Array.Find(allFish.fishSOCollection, element => element.name == fishName);
            fishInCooler.Add(result, dict[fishName]);
        }
    }
}
