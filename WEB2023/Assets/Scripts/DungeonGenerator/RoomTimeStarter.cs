using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTimeStarter : MonoBehaviour
{

    //public GameObject item;
    public List<GameObject> items = new List<GameObject>();
    public float duration = 0.3f;

    private Renderer itemRenderer;
    private ParticleSystem particleItem;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (var item in items)
        {
            Renderer itemRenderer = item.GetComponent<Renderer>();
            ParticleSystem particleItem = item.GetComponent<ParticleSystem>();
            particleItem.Stop();
            //StartCoroutine(ItemReduction(itemRenderer));
        }

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
            //StartCoroutine(ItemReduction());

            foreach (var item in items)
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();

                StartCoroutine(ItemReduction(itemRenderer));
                ParticleSystem particleItem = item.GetComponent<ParticleSystem>();
                particleItem.Play();
            }

        }
    }

    IEnumerator ItemReduction(Renderer itemRenderer)
    {
        float startTime = Time.time; // Guardar el tiempo de inicio
        Color originalColor = itemRenderer.material.color; // Guardar el color original

        while (Time.time - startTime < duration)
        {
            float elapsedTime = Time.time - startTime;
            float percentageCompleted = elapsedTime / duration;

            // Interpolación lineal para modificar la opacidad gradualmente
            Color lerpedColor = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0f), percentageCompleted);
            if(itemRenderer != null)
                itemRenderer.material.color = lerpedColor;

            yield return null;
        }
        if (itemRenderer != null)
            Destroy(itemRenderer.gameObject); // Destruir el objeto después de reducir la opacidad
    }


    //IEnumerator ItemReduction()
    //{

    //    yield return new WaitForSeconds(timeRemainging);
    //    Destroy(item);
    //}

}
