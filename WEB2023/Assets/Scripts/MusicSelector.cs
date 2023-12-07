using BehaviourAPI.UnityToolkit.Demos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private AudioSource OstPiso1;
    [SerializeField] public AudioSource OstPiso2;
    [SerializeField] private AudioSource OstPiso3; 
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovementInputSystem>().nivel == 0 && !OstPiso1.isPlaying)
        {
            OstPiso1.Play();
            OstPiso2.Stop();
            OstPiso3.Stop();
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 1 && !OstPiso2.isPlaying)
        {
            OstPiso1.Stop();
            OstPiso2.Play();
            OstPiso3.Stop();
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 2 && !OstPiso3.isPlaying)
        {
            OstPiso1.Stop();
            OstPiso2.Stop();
            OstPiso3.Play();
        }   
    }
}
