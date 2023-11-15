using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTimeStarter : MonoBehaviour
{

    public GameObject item;
    public int timeRemainging;
    public float duration = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //comienza tiempo
            StartCoroutine(ItemReduction());

        }
    }

    IEnumerator ItemReduction()
    {
        Vector2 startScale = item.transform.localScale;
        Vector2 endScale = Vector2.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            item.transform.localScale = Vector2.Lerp(startScale, endScale, currentTime / duration);
        }
        yield return new WaitForSeconds(timeRemainging);
        Destroy(item);
    }



}
