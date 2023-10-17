using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarMovil : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }
}
