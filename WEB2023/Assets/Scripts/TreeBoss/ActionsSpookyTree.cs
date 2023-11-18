using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsSpookyTree : MonoBehaviour
{
    [SerializeField] private GameObject spookyTree;
    // [SerializeField] private GameObject player;
    private GameObject player;
    [SerializeField] private GameObject EnemyBulletLeaf;
    [SerializeField] private GameObject Ghost;
    [SerializeField] private GameObject RootAttack;
    [SerializeField] private GameObject Root;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform finalPos1;
    [SerializeField] private Transform finalPos2;
    [SerializeField] private Transform finalPos3;
    //[SerializeField] private GameObject prueba;
    GameObject leaveFinalPos;
     bool ended;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #region MethodsSleep
    public void StartMethodSleep()
    {
        ended = false;
        animator.Play("Awake");
        StartCoroutine(WaitThreeSecondsAndEnd());
    }
    public Status UpdateMethodSleep()
    {
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }

    #endregion
    //Completed
    #region MethodsSharpLeavesP1
    public void StartMethodSharpLeavesP1()
    {
        Debug.Log("SHARP LEAVES P1");
        //instancia unos objetos bala que se muevan hacia delante mediante una corrutina
        ended = false;
        SpawnRootIfNotActive();
        Quaternion lookDown = Quaternion.LookRotation(Vector3.down);
        Vector3 posBullet1 = new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet2 = new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet3 = new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        StartCoroutine(InstantiateBullet(posBullet1, finalPos2, 8));
        StartCoroutine(InstantiateBullet(posBullet2, finalPos1, 8));
        StartCoroutine(InstantiateBullet(posBullet3, finalPos3, 8));
        //cuando termine cambia la variable de ended a true
        StartCoroutine(WaitThreeSecondsAndEnd());

    }
    public Status UpdateMethodSharpLeavesP1()
    {
        //crea lo de las variables
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }
    #endregion
    //Completed
    #region MethodsSharpLeavesP2
    public void StartMethodSharpLeavesP2()
    {
        Debug.Log("sharp leaves p2");
        //instancia unos objetos bala que se muevan hacia delante mediante una corrutina
        SpawnRootIfNotActive();
        ended = false;
        Vector3 posBullet1 = new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet2 = new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet3 = new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        StartCoroutine(InstantiateBullet(posBullet1, finalPos3,10));
        StartCoroutine(InstantiateBullet(posBullet2, finalPos1, 8));
        StartCoroutine(InstantiateBullet(posBullet3, finalPos2, 10));
        //cuando termine cambia la variable de ended a true
        StartCoroutine(WaitThreeSecondsAndEnd());
    }
    public Status UpdateMethodSharpLeavesP2()
    {
        //crea lo de las variables
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }
    #endregion 
    #region MethodsGhostSpawn
    public void StartMethodGhostSpawn()
    {
        Debug.Log("ghost spawn");
        SpawnRootIfNotActive();
        ended = false;
        //instancia unos fantasmas en momentos diferentes y en lugares diferentes
        int x = new System.Random().Next(-10, 10);
        int y = new System.Random().Next(-6, -2); 
        int x1 = new System.Random().Next(-10, 10);
        int y1 = new System.Random().Next(-6, -2);
        int x2 = new System.Random().Next(-10, 10);
        int y2 = new System.Random().Next(-6, -2);
        Vector3 RandomGhostPos = new Vector3(spookyTree.transform.position.x + x, spookyTree.transform.position.y + y, spookyTree.transform.position.z);
        Vector3 RandomGhostPos1 = new Vector3(spookyTree.transform.position.x + x1, spookyTree.transform.position.y + y1, spookyTree.transform.position.z);
        Vector3 RandomGhostPos2 = new Vector3(spookyTree.transform.position.x + x2, spookyTree.transform.position.y + y2, spookyTree.transform.position.z);
        StartCoroutine(InstantiateGhost(RandomGhostPos));
        StartCoroutine(InstantiateGhost(RandomGhostPos1));
        StartCoroutine(InstantiateGhost(RandomGhostPos2));
        //cuando termine cambia la variable de ended a true
        StartCoroutine(WaitThreeSecondsAndEnd());
    }
    public Status UpdateMethodGhostSpawn()
    {
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }
    #endregion
    #region MethodsSharpRoots
    public void StartMethodSharpRoots()
    {
        Debug.Log("sharp roots");
        ended = false;
        SpawnRootIfNotActive();
        int prob = new System.Random().Next(1, 3);
        //instancia unas raices con su animacion en diferentes sitios y momentos
        if (prob == 1) { 
            StartCoroutine(SpawnRootAtSecond(3));
        }
        else { 
            SpawnPattern3();
        }
        //cuando termine cambia la variable de ended a true
        StartCoroutine(WaitThreeSecondsAndEnd());
    }
    public Status UpdateMethodSharpRoots()
    {
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }
    #endregion
    #region MethodsRootsOut

    public void StartMethodRootsOut()
    {
        Debug.Log("roots out");
        ended = false;
        StartCoroutine(WaitOneSecondsAndEnd());
    }
    public Status UpdateMethodRootsOut()
    {
        if (ended)
            return Status.Success;
        else
            return Status.Running;
    }
    #endregion

    IEnumerator InstantiateBullet(Vector2 position, Transform finalPos, int bulletSpeed)
    {
        
        EnemyBulletLeaf.GetComponent<BulletController>().lifeTime = 3;
        EnemyBulletLeaf.GetComponent<BulletController>().bulletSpeed = bulletSpeed;
        GameObject bullet = Instantiate(EnemyBulletLeaf, position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BulletController>().GetPlayer(finalPos);
 //se manda la posicion a donde va a ir la bala
        yield return null;
    }
    IEnumerator InstantiateGhost(Vector2 position)
    {
        GameObject ghost = Instantiate(Ghost, position, Quaternion.identity) as GameObject;
        yield return null;
    }
    IEnumerator SpawnRootAtSecond(int second)
    {
        SpawnPattern1();
        yield return new WaitForSeconds(second);
        SpawnPattern2();
        yield return new WaitForSeconds(second);
        //second = 1;
        //pattern 2
        //second = 5
    }
    private void SpawnRootIfNotActive()
    {
        //si no hay ningun objeto en la escena que tenga el tag root
        int x = new System.Random().Next(-10, 10);
        int y = new System.Random().Next(-6, -4);
        if (GameObject.FindGameObjectWithTag("Root") == null)
        {
            Instantiate(Root, new Vector3(spookyTree.transform.position.x+ x,spookyTree.transform.position.y+ y,0), Quaternion.identity);
        }
    }
    private void SpawnPattern1()
    {
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y - 4, 0), Quaternion.identity);//der
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y - 2, 0), Quaternion.identity);//centroarr
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y - 8, 0), Quaternion.identity);//centroab
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y - 4, 0), Quaternion.identity);//centro
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y - 4, 0), Quaternion.identity);//izq
    }
    private void SpawnPattern2()
    {
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y - 2, 0), Quaternion.identity);//izarr
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x - 4 , spookyTree.transform.position.y - 8, 0), Quaternion.identity);//izabb
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x + 4 , spookyTree.transform.position.y - 8, 0), Quaternion.identity);//centroab
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y - 4, 0), Quaternion.identity);//centro
        Instantiate(RootAttack, new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y - 2, 0), Quaternion.identity);//der
    }
    private void SpawnPattern3()
    {
        StartCoroutine(WaitSecondAndAttack(1));
        StartCoroutine(WaitSecondAndAttack(2));
        StartCoroutine(WaitSecondAndAttack(3));
    }
    IEnumerator WaitThreeSecondsAndEnd() { yield return new WaitForSeconds(3); ended = true; }
    IEnumerator WaitOneSecondsAndEnd() { yield return new WaitForSeconds(1); ended = true; }
    IEnumerator WaitSecondAndAttack(int time) { yield return new WaitForSeconds(time); Instantiate(RootAttack, player.transform.position, Quaternion.identity);}

    private void Start()
    {
        ended = false;
        //leaveFinalPos = new GameObject();
        //leaveFinalPos.transform.position = new Vector3(EnemyBullet.transform.position.x, EnemyBullet.transform.position.y + 10, EnemyBullet.transform.position.z); //la pos a donde va a ir la bala
    }
}
