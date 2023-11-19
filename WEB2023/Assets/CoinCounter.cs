using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI expeditionMoneyTxt;

    [SerializeField]
    public TextMeshProUGUI totalMoneyTxt;

    public int expeditionMoney { get; set; }

    public int totalMoney { get; set; } = 1000;

    //Debug tienda
    //public int totalMoney { get; set; } = 100;

    private void Awake()
    {
        //expeditionMoney = 100;
        expeditionMoney = 0;
    }

    //Dinero expedición
    private void UpdateExpeditionMoneyText()
    {
        expeditionMoneyTxt.text = $"{expeditionMoney}";
    }

    public void ExpeditionMoneyChanger(int value)
    {
        expeditionMoney += value;
        UpdateExpeditionMoneyText();
    }

    public void ResetExpeditionMoney()
    {
        expeditionMoney = 0;
        UpdateExpeditionMoneyText();
    }

    //Dinero Total
    public void UpdateTotalMoneyText()
    {
        totalMoneyTxt.text = $"{totalMoney}";
    }

    public void TotalMoneyChanger(int value, float percentage)
    {
        totalMoney += (int)(percentage * value);  //1 - "percentage" es lo que conserva el jugador del total de dinero ganado
        UpdateTotalMoneyText();
    }

    public void ResetTotalMoney()
    {
        TotalMoneyChanger(0, 0);
    }
}
