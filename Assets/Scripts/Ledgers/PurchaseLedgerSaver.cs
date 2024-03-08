using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseLedgerSaver : MonoBehaviour
{
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/Cooler.json";
    }

    public void Save(Dictionary<string,int> purchaseLedger)
    {
        ListOfFishNames listOfFishNames = new ListOfFishNames();
        //iterate over all fish in the dictionary
        foreach(string fishname in purchaseLedger.Keys)
        {
            //add the fish name multiple times depending on quantity
            for(int i = 0; i < purchaseLedger[fishname]; i++)
            {
                listOfFishNames.fishNames.Add(fishname);
            }
        }
        string json = JsonUtility.ToJson(listOfFishNames);
        System.IO.File.WriteAllText(path, json);
    }

    public Dictionary<string, int> Load()
    {
        Dictionary<string,int> purchaseLedger = new Dictionary<string, int>();
        ListOfFishNames listOfFishNames = new ListOfFishNames();
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, listOfFishNames);

        //unpack list into dict
        foreach(string fishName in listOfFishNames.fishNames)
        {
            //item already exists in array so increment
            if(!purchaseLedger.TryAdd(fishName, 1))
            {
                purchaseLedger[fishName] += 1;
            }
        }

        return purchaseLedger;
    }

    [System.Serializable]
    public class ListOfFishNames
    {
        public List<string> fishNames = new List<string>();
    }

}
