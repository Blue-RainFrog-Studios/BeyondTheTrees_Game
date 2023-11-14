using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMoney : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        moneyText.text = $"HAS OBTENIDO {player.GetComponent<CoinCounter>().expeditionMoney} DE ORO";
        player.GetComponent<KnightScript>().MoneyDealer(0.3f);  //El jugador conserva el 30% de lo que ha ganado si pierde
    }
}
