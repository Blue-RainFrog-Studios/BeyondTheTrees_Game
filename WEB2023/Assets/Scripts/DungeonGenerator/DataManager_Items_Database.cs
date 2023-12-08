using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_Items_Database : MonoBehaviour
{
    private static DataManager_Items_Database instance;

    // Referencia al ScriptableObject que deseas compartir
    public All_Items_Database myItemsData;

    // Propiedad para acceder a la instancia del Singleton
    public static DataManager_Items_Database Instance
    {
        get
        {
            if (instance == null)
            {
                // Si no hay una instancia, intenta encontrarla en la escena
                instance = FindObjectOfType<DataManager_Items_Database>();

                // Si aún no se encuentra, crea un nuevo objeto y agrega el componente
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("DataManager_Items_Database");
                    instance = singletonObject.AddComponent<DataManager_Items_Database>();
                }
            }

            return instance;
        }
    }

    // Otras funciones y métodos relacionados con el manejo del ScriptableObject

    private void Awake()
    {
        // Asegurar que solo haya una instancia del Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



}
