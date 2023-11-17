using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loanding : MonoBehaviour
{

    public GameObject loanding;
    public GameObject noInve;
    private bool once = true;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        //loanding.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Basement" && once)
        {
            loanding.SetActive(true);
            noInve.SetActive(false);
            this.gameObject.GetComponent<PlayerMovementInputSystem>().enabled = false;
            StartCoroutine(WaitLoad());
            
        }
        
    }
    IEnumerator WaitLoad()
    {

        if (RoomController.iHaveFinishied)
        {
            yield return new WaitForSeconds(4);
            loanding.SetActive(false);
            noInve.SetActive(true);
            this.gameObject.GetComponent<PlayerMovementInputSystem>().enabled = true;
            once = false;

        }
       
    }
}
