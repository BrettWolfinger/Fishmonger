using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CoolerManager : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] FishSO fish;
    Dictionary<FishSO,int> dict = new Dictionary<FishSO,int>();
    Button newButton;

    void Awake()
    {
        dict.Add(fish,2);
    }

    void Start()
    {
        foreach(FishSO fish in dict.Keys)
        {
            newButton = Instantiate(button,gameObject.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + dict[fish];
            newButton.onClick.AddListener(delegate {SpawnFish(fish,newButton); });
        }
    }

    void SpawnFish(FishSO fish, Button button)
    {
        if(dict[fish] > 0)
        {
            dict[fish] -= 1;
            if(dict[fish] == 0)
            {
                Destroy(button.gameObject);
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + dict[fish];
            }
            
            Instantiate(fish.GetModelPrefab(), new Vector3(0,0,0),fish.GetModelPrefab().transform.rotation);
        }
    }
}
