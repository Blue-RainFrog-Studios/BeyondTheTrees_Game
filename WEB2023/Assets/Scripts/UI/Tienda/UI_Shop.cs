using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Shop : MonoBehaviour
{
    private Transform contenedor;
    private Transform plantillaObjetoTienda;

    private void Awake()
    {
        contenedor = transform.Find("Contenedor");
        plantillaObjetoTienda = contenedor.Find("Plantilla Objeto Tienda");
        plantillaObjetoTienda.gameObject.SetActive(false);
    }
    private void CrearBotonTienda(Sprite spriteObjeto, string nombreObjeto, int precioObjeto, int posicion)
    {
        Transform tiendaObjetosTransform = Instantiate(plantillaObjetoTienda, contenedor);
        RectTransform tiendaObjetosRectTransform = tiendaObjetosTransform.GetComponent<RectTransform>();
        float alturaTiendaObjetos = 30f;
        tiendaObjetosRectTransform.anchoredPosition = new Vector2(0, -alturaTiendaObjetos * posicion);


    }
}
