using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loanding : MonoBehaviour
{

    public GameObject loanding;

    // Start is called before the first frame update

    private void Awake()
    {
        loanding.SetActive(true);

    }
    void Start()
    {
        loanding.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(WaitLoad());
    }
    IEnumerator WaitLoad()
    {
        if (RoomController.iHaveFinishied)
        {
            yield return new WaitForSeconds(4);
            loanding.SetActive(false);
        }
    }
}
