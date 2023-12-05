using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skull;



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<EnemyController>().life <= 0)
            {
                if(skull != null) 
                {
                    if (skull.GetComponent<Skull>().valor == 9)
                    {
                        skull.GetComponent<Animator>().SetTrigger("Fase 1-2");
                        skull.GetComponent<Skull>().valor = 6;
                    }
                    else if (skull.GetComponent<Skull>().valor == 6)
                    {
                        skull.GetComponent<Animator>().SetTrigger("Fase 2-3");
                        skull.GetComponent<Skull>().valor = 3;
                    }
                    else if (skull.GetComponent<Skull>().valor == 3)
                    {
                        skull.GetComponent<Animator>().SetTrigger("Fase 3-4");
                        skull.GetComponent<Skull>().valor = 0;
                    }
                }               
            }
        }
    }
}
