using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarMovil2 : MonoBehaviour
{
    private void Start()
    {
        //gameObject.SetActive(Application.isMobilePlatform);
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Debug.Log("Puton");
            //Input.GetAxis
            //gameObject.transform.position = 
            //gameObject.transform.localPosition = Input.mousePosition;
            Touch touch = Input.GetTouch(0);
            gameObject.transform.position = touch.position;//Input.mousePosition;
        }
    }
   
}
