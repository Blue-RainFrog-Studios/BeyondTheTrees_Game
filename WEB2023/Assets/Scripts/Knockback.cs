using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D RigidbodyComponent;

    [SerializeField] private float strength = 10f, delay = 0.15f;
    //se crean 2 eventos 
    public UnityEvent OnBegin, OnDone;
    Vector2 direction;
    Rigidbody2D rbGlobal;
    public void PlayFeedback(GameObject player, Rigidbody2D rb)
    {
        rbGlobal = rb;
        OnDone?.Invoke();
        direction = transform.position - player.transform.position;
        direction = direction.normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rbGlobal.velocity = Vector2.zero; //posibilidad que esto sea llamado sin haberse asignado el rb
        OnDone?.Invoke();
    }
}
