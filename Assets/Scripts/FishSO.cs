using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishmonger/CreateNewFish", order = 0)]
public class FishSO : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;
    [SerializeField] int buyPrice;
    [SerializeField] int numOfFilets;
    [SerializeField] int salePricePerFilet;
    
    [field: SerializeField] public int salePriceDecreaseNotDescaled {get;private set;}
    
    [field:SerializeField] public int salePriceDecreaseNotDeboned {get;private set;}
    
    [field:SerializeField] public int salePriceDecreaseNotSkinned {get; private set;}
    [Tooltip("When calculating price change, will generated random int between -value and +value inclusive")]
    [SerializeField] int salePriceVariationRange;


    public GameObject GetModelPrefab() 
    {
        return modelPrefab;
    }

    public int GetBuyPrice()
    {
        return buyPrice;
    }
    public int GetSalePrice()
    {
        return salePricePerFilet;
    }

    public int GetNumFilet()
    {
        return numOfFilets;
    }
}
