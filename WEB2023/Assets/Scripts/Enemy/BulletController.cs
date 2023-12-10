using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    public int damage=10;
    public int bulletSpeed = 0;

    private Vector2 lastPos;

    private Vector2 currentPos;

    private Vector2 playerPos;

    void Start()
    {
        //make the bullet look towards the player in two dimensions + 90 degrees
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg + 90);

    }

    // Update is called once per frame
    void Update()
    {
        
        currentPos= transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, bulletSpeed * Time.deltaTime);
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
        if(collision.CompareTag("Prop"))
        {
            
            Destroy(gameObject);
        }
       
    }
}
