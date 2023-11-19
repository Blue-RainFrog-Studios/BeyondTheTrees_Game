using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMoney : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    int money;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<CoinCounter>().expeditionMoney;
        moneyText.text = $"Has obtenido {(int)(player.GetComponent<CoinCounter>().expeditionMoney * 0.3f)} Kero Coins";
        player.GetComponent<KnightScript>().MoneyDealer(0.3f, money);   //El jugador conserva el 30% de lo que ha ganado si pierde
    }
}
