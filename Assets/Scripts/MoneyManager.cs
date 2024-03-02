using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    int moneyTotal = 50;

    void Awake()
    {
        moneyText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateMoneyText();
    }

    public void SubtractMoney(int amountToSubtract)
    {
        moneyTotal -= amountToSubtract;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "Money: $" + moneyTotal.ToString();
    }

    public int GetMoneyTotal()
    {
        return moneyTotal;
    }
}
