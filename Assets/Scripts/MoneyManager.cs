using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    int moneyTotal = 50;
    string path;
    MoneySave moneySave = new MoneySave();

    void Awake()
    {
        path = Application.persistentDataPath + "/Money.json";
        moneyText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Load();
    }

    void Start()
    {
        UpdateMoneyText();
    }

    public void SubtractMoney(int amountToSubtract)
    {
        moneyTotal -= amountToSubtract;
        UpdateMoneyText();
        Save();
    }
    
    public void AddMoney(int amountToAdd)
    {
        moneyTotal += amountToAdd;
        UpdateMoneyText();
        Save();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "Money: $" + moneyTotal.ToString();
    }

    public int GetMoneyTotal()
    {
        return moneyTotal;
    }

    void Save()
    {
        moneySave.money = moneyTotal;
        string json = JsonUtility.ToJson(moneySave);
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
        JsonUtility.FromJsonOverwrite(json, moneySave);
        moneyTotal = moneySave.money;
    }

    public void ResetMoneySave()
    {
        moneyTotal = 50;
        Save();
        UpdateMoneyText();
    }

    [System.Serializable]
    public class MoneySave
    {
        public int money;
    }
}
