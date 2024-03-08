using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishCollection", menuName = "Fishmonger/CreateNewFishCollection", order = 0)]
public class FishSOCollection : ScriptableObject
{
    [field: SerializeField] public FishSO[] fishSOCollection {get;private set;}
}
