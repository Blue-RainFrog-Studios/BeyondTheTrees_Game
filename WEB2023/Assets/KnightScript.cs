using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightScript : MonoBehaviour
{

    [SerializeField]
    public Slider lifeBar;

    KnightScript knight;

    public int health { get; set; }
    public float speed { get; set; }

    public int attack { get; set; }
    public int defense { get; set; }

    public KnightScript() { 
            health = 50;
            speed = 6;
            attack = 10;
            defense = 7;
        }
        
    

    private void Awake()
    {
        knight = new();
    }
    public void ReceiveAttack(int dmgValue)
    {

            knight.health -= (dmgValue - knight.defense);
            lifeBar.value = knight.health;
        if (knight.health <= 0)
                DeleteAll();
                SceneManager.LoadScene("GameOver");
        
    }
    public void DeleteAll()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
    }
}
