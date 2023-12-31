using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Shop : MonoBehaviour
{
    private Transform contenedor;
    private Transform plantillaObjetoTienda;

    private void Awake()
    {
        contenedor = transform.Find("Contenedor");
        plantillaObjetoTienda = contenedor.Find("Plantilla Objeto tienda");
        plantillaObjetoTienda.gameObject.SetActive(true);
    }

    private void Start()
    {
        CrearBotonTienda(PoolItems.GetSprite(PoolItems.NombreItem.DagaSolar), "Daga Solar", PoolItems.GetPrecio(PoolItems.NombreItem.DagaSolar), 0);
        CrearBotonTienda(PoolItems.GetSprite(PoolItems.NombreItem.DagaLunar), "Daga Lunar", PoolItems.GetPrecio(PoolItems.NombreItem.DagaLunar), 1);
    }
    private void CrearBotonTienda(Sprite spriteObjeto, string nombreObjeto, int precioObjeto, int posicion)
    {
        Transform tiendaObjetosTransform = Instantiate(plantillaObjetoTienda, contenedor);
        RectTransform tiendaObjetosRectTransform = tiendaObjetosTransform.GetComponent<RectTransform>();
        float alturaTiendaObjetos = 100f;
        tiendaObjetosRectTransform.anchoredPosition = new Vector2(0, -alturaTiendaObjetos * posicion);

        tiendaObjetosTransform.Find("Nombre Item").GetComponent<TextMeshProUGUI>().SetText(nombreObjeto);
        tiendaObjetosTransform.Find("Precio").GetComponent<TextMeshProUGUI>().SetText(precioObjeto.ToString());

        tiendaObjetosTransform.Find("Imagen Item").GetComponent<Image>().sprite = spriteObjeto;


    }
}
