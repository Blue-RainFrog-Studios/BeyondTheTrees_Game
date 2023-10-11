using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJugar : MonoBehaviour
{
    public void CambiarEscena(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
