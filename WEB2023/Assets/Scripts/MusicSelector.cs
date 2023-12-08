using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] public AudioSource OstPiso1;
    [SerializeField] public AudioSource OstPiso2;
    [SerializeField] public AudioSource OstPiso3; 
    GameObject player;
    private bool played = false;
    private bool played1 = false;
    private bool played2 = false;
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovementInputSystem>().nivel == 0 && !OstPiso1.isPlaying && !played)
        {
            OstPiso1.Play();
            OstPiso2.Stop();
            OstPiso3.Stop(); 
            played = true;
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 1 && !OstPiso2.isPlaying && !played1)
        {
            OstPiso1.Stop();
            OstPiso2.Play();
            OstPiso3.Stop();
            played1 = true;
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 2 && !OstPiso3.isPlaying && !played2)
        {
            OstPiso1.Stop();
            OstPiso2.Stop();
            OstPiso3.Play();
            played2 = true;
        }
    }
}
