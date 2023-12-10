using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBag : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Attack")) return;
        audioSource.PlayOneShot(audioClip);
        Destroy(collision.gameObject);
    }

}
