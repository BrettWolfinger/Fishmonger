using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI summaryScreenEntry;
    [SerializeField] TextMeshProUGUI totalSales;
    List<FiletManager.Filet> list;
    SaleManager saleManager;

    void Awake()
    {
        saleManager = FindObjectOfType<SaleManager>();
    }

    void Start()
    {
        list = saleManager.GetList();
        GenerateSummary();
    }

    private void GenerateSummary()
    {
        TextMeshProUGUI textEntry;
        foreach(FiletManager.Filet filet in list)
        {
            textEntry = Instantiate(summaryScreenEntry,gameObject.transform);
            textEntry.text = filet.ToString();
        }

        totalSales.text = "Total Sales: $" + saleManager.GetTotalSales().ToString();
    }
}
