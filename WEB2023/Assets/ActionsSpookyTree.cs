using BehaviourAPI.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionsSpookyTree : MonoBehaviour
{
    [SerializeField] private GameObject spookyTree;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject EnemyBullet;
    GameObject leaveFinalPos;
     bool ended;
    #region MethodsSleep
    public void StartMethodSleep()
    {
        Debug.Log("SLEEP");
       spookyTree.SetActive(false);
    }
    public Status UpdateMethodSleep()
    {
        return Status.Success;
    }
    public void StopMethodSleep()
    {
        spookyTree.SetActive(true);
    }
    #endregion
    #region MethodsSharpLeavesP1
    public void StartMethodSharpLeavesP1()
    {
        Debug.Log("SHARP LEAVES P1");
        //instancia unos objetos bala que se muevan hacia delante mediante una corrutina
        ended = false;
        Quaternion lookDown = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        Vector3 posBullet1 = new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet2 = new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet3 = new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        StartCoroutine(InstantiateBullet(posBullet1, lookDown));
        StartCoroutine(InstantiateBullet(posBullet2, lookDown));
        StartCoroutine(InstantiateBullet(posBullet3, lookDown));
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
    #region MethodsSharpLeavesP2
    public void StartMethodSharpLeavesP2()
    {
        Debug.Log("sharp leaves p2");   
        //instancia unos objetos bala que se muevan hacia delante mediante una corrutina
        ended = false;
        Quaternion lookDown = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        Quaternion lookLeft = Quaternion.Euler(0f, -45f, 0f) * Quaternion.LookRotation(Vector3.down);
        Quaternion lookRight = Quaternion.Euler(0f, 45f, 0f) * Quaternion.LookRotation(Vector3.down);
        Vector3 posBullet1 = new Vector3(spookyTree.transform.position.x + 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet2 = new Vector3(spookyTree.transform.position.x, spookyTree.transform.position.y, spookyTree.transform.position.z);
        Vector3 posBullet3 = new Vector3(spookyTree.transform.position.x - 4, spookyTree.transform.position.y, spookyTree.transform.position.z);
        StartCoroutine(InstantiateBullet(posBullet1, lookLeft));
        StartCoroutine(InstantiateBullet(posBullet2, lookDown));
        StartCoroutine(InstantiateBullet(posBullet3, lookRight));
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
        //instancia unos fantasmas en momentos diferentes y en lugares diferentes
        //cuando termine cambia la variable de ended a true
    }
    public Status UpdateMethodGhostSpawn()
    {
        //crea lo de las variables
        return Status.Running;
    }
    #endregion
    #region MethodsSharpRoots
    public void StartMethodSharpRoots()
    {
        //instancia unas raices con su animacion en diferentes sitios y momentos
        //cuando termine cambia la variable de ended a true
    }
    public Status UpdateMethodSharpRoots()
    {
        //crea lo de las variables
        return Status.Running;
    }
    #endregion
    #region MethodsRootsOut

    public void StartMethodRootsOut()
    {
        Debug.Log("roots out");
        //instancia unas raices que el jugador pueda golpear
        //cuando el jugador mate la raiz cambia la variable de ended a true
    }
    public Status UpdateMethodRootsOut()
    {
        //crea lo de las variables
        return Status.Success;
    }
    #endregion

    IEnumerator InstantiateBullet(Vector2 position, Quaternion direction)
    {
        EnemyBullet.GetComponent<BulletController>().lifeTime = 3;
        GameObject bullet = Instantiate(EnemyBullet, position, direction) as GameObject;
        leaveFinalPos.transform.rotation = EnemyBullet.transform.rotation;
        leaveFinalPos.transform.position = EnemyBullet.transform.forward*10; //la pos a donde va a ir la bala
        bullet.GetComponent<BulletController>().GetPlayer(leaveFinalPos.transform); //se manda la posicion a donde va a ir la bala
        yield return null;
    }
    IEnumerator WaitThreeSecondsAndEnd() { yield return new WaitForSeconds(3); ended = true; }

    private void Start()
    {
        ended = false;
        leaveFinalPos = new GameObject();
    }
}
