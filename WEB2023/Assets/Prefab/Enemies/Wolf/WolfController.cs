using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float speedJump;
    [SerializeField] public int damage;
    public int initialHP;
    [SerializeField] public float HP;
    GameObject player;
    private bool Played;
    private bool PlayedSF;

    private void Start()
    {
        HP = initialHP;
    }
}