using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarMovil3 : MonoBehaviour
{
    private Vector3 posicion;

    private void Start()
    {
        posicion = transform.position;
        Input.multiTouchEnabled = true;
        Input.simulateMouseWithTouches = true;
    }
    private void Update()
    {

        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.GetTouch(0);
            float touchXPercentage = touch.position.x / Screen.width;
            float touchYPercentage = touch.position.y / Screen.height;
            //if(!GetComponent<Image>().enabled)

            if (touchXPercentage > 0.5f)
            {
                GetComponent<Image>().enabled = true;
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    //Touch touch = Input.GetTouch(0);
                    gameObject.transform.position = touch.position;
                    posicion = touch.position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    //if (GetComponent<Image>().enabled)
                    GetComponent<Image>().enabled = false;
                }
            }
        }

    }
}
