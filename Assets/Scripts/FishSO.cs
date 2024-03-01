using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishmonger/CreateNewFish", order = 0)]
public class FishSO : ScriptableObject
{
    [SerializeField] GameObject modelPrefab;

    public GameObject GetModelPrefab() 
    {
        return modelPrefab;
    }

}
