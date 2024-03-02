using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishmonger/CreateNewFish", order = 0)]
public class FishSO : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;
    [SerializeField] int buyPrice;
    [SerializeField] int salePrice;

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
        return salePrice;
    }

}
