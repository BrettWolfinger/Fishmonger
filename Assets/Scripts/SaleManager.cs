using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaleManager : MonoBehaviour
{
    List<FiletManager.Filet> list = new List<FiletManager.Filet>();
    string path;
    ListOfSales listOfSales = new ListOfSales();
    DayManager dayManager;
    MoneyManager moneyManager;

    void Awake() 
    {
        path = Application.persistentDataPath + "/Record.json";
        dayManager = FindObjectOfType<DayManager>();
        moneyManager = FindObjectOfType<MoneyManager>();

        Scene currentScene = SceneManager.GetActiveScene ();
        if (currentScene.name == "PostGameplay") 
		{
			Load();
		}
    }

    public void Record(FiletManager.Filet filet)
    {
        list.Add(filet);
        Save();
    }

    public void EndDay()
    {
        dayManager.IncrementDay();
        moneyManager.AddMoney(GetTotalSales());
    }

    public List<FiletManager.Filet> GetList()
    {
        return list;
    }


    public int GetTotalSales()
    {
        int saleTotal = 0;
        foreach(FiletManager.Filet filet in list)
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
        public List<FiletManager.Filet> filets;
    }
}
