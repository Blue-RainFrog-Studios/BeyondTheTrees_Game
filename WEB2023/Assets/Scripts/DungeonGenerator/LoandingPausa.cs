using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoandingPausa : MonoBehaviour
{

    public GameObject noPausa;

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
            noPausa.SetActive(false);
            
            StartCoroutine(WaitLoad());
            
        }
        
    }
    IEnumerator WaitLoad()
    {

        if (RoomController.iHaveFinishied)
        {
            yield return new WaitForSeconds(4);

            noPausa.SetActive(true);
            once = false;

        }
       
    }
}
