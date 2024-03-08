using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaleLedger : MonoBehaviour
{
    public List<FiletModel.FiletStruct> list {get; private set;}
    string path;
    ListOfSales listOfSales = new ListOfSales();
    DayManager dayManager;
    MoneyManager moneyManager;

    void Awake() 
    {
        path = Application.persistentDataPath + "/Record.json";
        dayManager = FindObjectOfType<DayManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
        list = new List<FiletModel.FiletStruct>();

        Scene currentScene = SceneManager.GetActiveScene ();
        if (currentScene.name == "PostGameplay") 
		{
			Load();
		}
    }

    void OnEnable()
    {
        FiletManager.FiletSold += Record;
        //GamePhaseManager.PurchasePhaseEnded += PurchasePhaseEnd;
    }

    void OnDisable()
    {
        FiletManager.FiletSold -= Record;
        //GamePhaseManager.PurchasePhaseEnded -= PurchasePhaseEnd;
    }

    public void Record(FiletModel.FiletStruct filet)
    {
        list.Add(filet);
        Save();
    }

    public void EndDay()
    {
        dayManager.IncrementDay();
        moneyManager.AddMoney(GetTotalSales());
    }

    public int GetTotalSales()
    {
        int saleTotal = 0;
        foreach(FiletModel.FiletStruct filet in list)
        {
            saleTotal += filet.salePrice;
        }

        return saleTotal;
    }

    void Save()
    {
        listOfSales.filets = list;
        string json = JsonUtility.ToJson(listOfSales);
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
        JsonUtility.FromJsonOverwrite(json, listOfSales);
        list = listOfSales.filets;
    }

    [System.Serializable]
    public class ListOfSales
    {
        public List<FiletModel.FiletStruct> filets;
    }
}
