using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiletManager : MonoBehaviour
{
    //type of fish this filet belongs to
    [SerializeField] FishSO fish;

    //booleans for different things players can chose to do/not do that
    //will affect the fish sale price
    bool isDescaled = false;
    bool isDeboned = false;
    bool isSkinned = false;
    public void Sell()
    {
    }
}
