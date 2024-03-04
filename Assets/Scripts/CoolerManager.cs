using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoolerManager : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] FishSO[] allFish;
    Dictionary<FishSO,int> dict = new Dictionary<FishSO,int>();
    Button newButton;
    string path;
    List<string> saveLoadList = new List<string>();
    ListOfFishNames listOfFishNames = new ListOfFishNames();
    CoolerAnimation coolerAnimation;

    void Awake()
    {
        path = Application.persistentDataPath + "/Cooler.json";
        // Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();
        coolerAnimation = gameObject.GetComponentInParent<CoolerAnimation>();

		if (currentScene.name == "GameplayScreen") 
		{
			Load();
		}
  
    }

    void Start()
    {
        foreach(FishSO fish in dict.Keys)
        {
            newButton = Instantiate(button,gameObject.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + dict[fish];
            newButton.onClick.AddListener(delegate {SpawnFish(fish,newButton); });
            newButton.onClick.AddListener(delegate {coolerAnimation.Slide();});
        }
    }

    public void Demo()
    {
        dict.Add(allFish[0],5);
        Save();
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

    public void AddFishToCooler(FishSO fish, int quantity)
    {
        //FishSO result = Array.Find(allFish, element => element.name == fishName);
        //item already exists in array so increment
        if(!dict.TryAdd(fish, quantity))
        {
            dict[fish] += quantity;
        }
        Save();
    }

    void Save()
    {
        saveLoadList.Clear();
        //iterate over all fish in the dictionary
        foreach(FishSO fish in dict.Keys)
        {
            //add the fish name multiple times depending on quantity
            for(int i = 0; i < dict[fish]; i++)
            {
                saveLoadList.Add(fish.name);
            }
        }
        listOfFishNames.fishNames = saveLoadList;
        string json = JsonUtility.ToJson(listOfFishNames);
        System.IO.File.WriteAllText(path, json);
    }

    void Load()
    {
        //If there is no save file that exists, create one
        if (!File.Exists(path))
        {
            Save();
        }
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, listOfFishNames);
        saveLoadList = listOfFishNames.fishNames;

        //unpack list into dict
        foreach(string fishName in saveLoadList)
        {
            FishSO result = Array.Find(allFish, element => element.name == fishName);
            //item already exists in array so increment
            if(!dict.TryAdd(result, 1))
            {
                dict[result] += 1;
            }
        }
    }

    [System.Serializable]
    public class ListOfFishNames
    {
        public List<string> fishNames;
    }

}
