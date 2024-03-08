using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolerController : CoolerElement
{
    public void SpawnFish(FishSO fish, Button button)
    {
        if(cooler.model.fishInCooler[fish] > 0)
        {
            cooler.model.fishInCooler[fish] -= 1;
            if(cooler.model.fishInCooler[fish] == 0)
            {
                Destroy(button.gameObject);
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + cooler.model.fishInCooler[fish];
            }
            
            Instantiate(fish.modelPrefab, new Vector3(0,0,0),fish.modelPrefab.transform.rotation);
        }
    }
}
