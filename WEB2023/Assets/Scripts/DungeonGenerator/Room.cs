using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{


    public float Width;
    public float Height;

    public int X;
    public int Y;
    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("You pressed play on the wrong scene!");
            return;
        }

        RoomController.instance.RegisterRoom(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentred()
    {
        return new Vector3(X * Width, Y * Height);
    }
}