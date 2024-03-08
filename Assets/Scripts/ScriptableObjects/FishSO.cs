using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Scriptable object for each of our unique fish. Contains modelPrefab
* and preset economic data for Day 1 of the game. Some of this data is likely
* to move out of here into other areas when later updates happen (Factions for example)
*/
[CreateAssetMenu(fileName = "NewFish", menuName = "Fishmonger/CreateNewFish", order = 0)]
public class FishSO : ScriptableObject
{
    [field:SerializeField] public GameObject modelPrefab {get;private set;}
    [field:SerializeField] public int buyPrice {get;private set;}
    [field:SerializeField] public int numOfFilets {get;private set;}
    [field:SerializeField] public int salePricePerFilet {get;private set;}
    
    [field: SerializeField] public int salePriceDecreaseNotDescaled {get;private set;}
    
    [field:SerializeField] public int salePriceDecreaseNotDeboned {get;private set;}
    
    [field:SerializeField] public int salePriceDecreaseNotSkinned {get; private set;}
    [Tooltip("When calculating price change, will generated random int between -value and +value inclusive")]
    [field:SerializeField] public int salePriceVariationRange {get;private set;}
    [field:SerializeField] public int stock {get; private set;}
}
