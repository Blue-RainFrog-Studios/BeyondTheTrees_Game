using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMovil2 : MonoBehaviour
{
    private Vector3 posicion;
    //private Touch touch;
    private void Start()
    {
        posicion = transform.position;
    }
    private void Update()
    {
        
        if (Input.touchCount > 0 )
        {
            Touch touch = Input.GetTouch(0);
            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);
            }
            
            float touchXPercentage = touch.position.x / Screen.width;
            float touchYPercentage = touch.position.y / Screen.height;
            //if(!GetComponent<Image>().enabled)
           
            if(touchXPercentage < 0.5f)
            {
                GetComponent<Image>().enabled = true;
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                if (touch.phase == TouchPhase.Began)
                {
                    //Touch touch = Input.GetTouch(0);
                    gameObject.transform.position = touch.position;
                    posicion = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    //gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    //if (GetComponent<Image>().enabled)
                    GetComponent<Image>().enabled = false;
                }
            } 
        }
       
    }


}
