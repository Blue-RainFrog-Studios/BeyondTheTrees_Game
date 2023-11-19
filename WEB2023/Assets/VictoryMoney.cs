using TMPro;
using UnityEngine;

public class VictoryMoney : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    int money;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        money = player.GetComponent<CoinCounter>().expeditionMoney;
        moneyText.text = $"Has obtenido {(int)(player.GetComponent<CoinCounter>().expeditionMoney * 1.0f)} Kero Coins";
        player.GetComponent<KnightScript>().MoneyDealer(1.0f, money);   //El jugador conserva el 100% de lo que ha ganado si gana
    }
}
