using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SupplyManager : MonoBehaviour
{

    [SerializeField] FishSO[] typesOfFish;
    public Dictionary<string,int> purchaseLedger {get;set;}
    public Dictionary<string,FishSupplyInfo> fishSupplyDatabase;
    ListOfSupplyInfo listOfSupplyInfo = new ListOfSupplyInfo();
    string path;

    void Awake() 
    {
        path = Application.persistentDataPath + "/SupplyData.json";
        Load();
    }

    public void ProcessPurchaseLedger()
    {
        foreach(FishSupplyInfo fishSupplyInfo in fishSupplyDatabase.Values)
        {
            if(purchaseLedger.ContainsKey(fishSupplyInfo.name))
            {
                fishSupplyInfo.pricePerFish += 1;
                fishSupplyInfo.stock += 1;
            }
            else
            {
                fishSupplyInfo.pricePerFish -= 1;
                fishSupplyInfo.stock -= 1;
            }
        }
        Save();
    }

    public FishSupplyInfo GetFishSupplyData(string fishName)
    {
        return fishSupplyDatabase[fishName];
    }

    void Save()
    {
        listOfSupplyInfo.DictToList(fishSupplyDatabase);
        string json = JsonUtility.ToJson(listOfSupplyInfo);
        System.IO.File.WriteAllText(path, json);
    }

    void Load()
    {
        //If there is no save file that exists, create one
        if (!File.Exists(path))
        {
            fishSupplyDatabase = new Dictionary<string, FishSupplyInfo>();
            //Load data from fish SOs
            foreach(FishSO fish in typesOfFish)
            {
                print(fish.name);
                fishSupplyDatabase[fish.name] = new FishSupplyInfo(fish.name,fish.GetBuyPrice(),fish.stock);
                print(fishSupplyDatabase.Count);
            }
            print(fishSupplyDatabase.Count);
            Save();
        }
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, listOfSupplyInfo);
        fishSupplyDatabase = listOfSupplyInfo.ReturnListToDict();
    }

    [System.Serializable]
    public class FishSupplyInfo
    {
        public string name;
        public int pricePerFish;
        public int stock;

        public FishSupplyInfo(string name,int pricePerFish,int stock)
        {
            this.name = name;
            this.pricePerFish = pricePerFish;
            this.stock = stock;
        }
    }

    [System.Serializable]
    public class ListOfSupplyInfo
    {
        public List<FishSupplyInfo> listOfSupplyInfo;

        public void DictToList(Dictionary<string,FishSupplyInfo> fishSupplyDatabase)
        {
            listOfSupplyInfo = new List<FishSupplyInfo>();
            foreach(FishSupplyInfo fishSupplyInfo in fishSupplyDatabase.Values)
            {
                listOfSupplyInfo.Add(fishSupplyInfo);
            }
        }

        public Dictionary<string,FishSupplyInfo> ReturnListToDict()
        {
             Dictionary<string,FishSupplyInfo> dict = new Dictionary<string, FishSupplyInfo>();
             foreach(FishSupplyInfo fishSupplyInfo in listOfSupplyInfo)
             {
                dict[fishSupplyInfo.name] = fishSupplyInfo;
             }
             return dict;
        }
    }
}
