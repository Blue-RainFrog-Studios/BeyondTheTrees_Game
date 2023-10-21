using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    public int damage=10;

    private Vector2 lastPos;

    private Vector2 currentPos;

    private Vector2 playerPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPos= transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
        if(currentPos==lastPos)
        {
            Destroy(gameObject);
        }
        lastPos=currentPos;
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<KnightScript>().ReceiveAttack(damage);
            Destroy(gameObject);
        }
       
    }
}
