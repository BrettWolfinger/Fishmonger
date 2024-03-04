using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    TextMeshProUGUI dayText;
    int dayNumber = 1;
    string path;
    DaySave daySave = new DaySave();

    void Awake()
    {
        path = Application.persistentDataPath + "/Day.json";
        dayText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Load();
    }

    void Start()
    {
        UpdateDayText();
    }

    public void IncrementDay()
    {
        dayNumber++;
        Save();
    }

    public void UpdateDayText()
    {
        dayText.text = "Day: " + dayNumber.ToString();
    }

    public int GetDayNumber()
    {
        return dayNumber;
    }
    
    void Save()
    {
        daySave.day = dayNumber;
        string json = JsonUtility.ToJson(daySave);
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
        JsonUtility.FromJsonOverwrite(json, daySave);
        dayNumber = daySave.day;
    }

    public void ResetDaySave()
    {
        dayNumber = 1;
        Save();
        UpdateDayText();
    }

    [System.Serializable]
    public class DaySave
    {
        public int day;
    }
}
