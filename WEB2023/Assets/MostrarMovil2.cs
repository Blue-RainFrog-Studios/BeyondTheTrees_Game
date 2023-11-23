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
        if(Input.anyKey && Application.isMobilePlatform)
        {
            //Input.GetAxis
            //gameObject.transform.position = 
            gameObject.SetActive(true);
        }
    }
}
