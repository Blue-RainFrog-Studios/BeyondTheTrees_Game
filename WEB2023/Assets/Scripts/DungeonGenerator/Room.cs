using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{


    //public float Width;
    public float Width;


    //public float Height;
    public float Height;

    public int X;
    public int Y;

    private bool updatedDoors = false;
    public Room(int x,int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public GameObject trees;


    public List<Door> doors = new List<Door>();

    [SerializeField] GameObject itemSpawner;
    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("You pressed play on the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doortDype)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                break;
                case Door.DoorType.left:
                    leftDoor = d;
                break;
                case Door.DoorType.top:
                    topDoor = d;
                break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                break;
            }        
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if(name.Contains("End") || name.Contains("End1") || name.Contains("End2") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

    }
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doortDype)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                        door.gameObject.SetActive(false);
                        door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                        door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                        door.doorCollider.SetActive(true);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                        door.doorCollider.SetActive(true);
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X+1,Y)){
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y+1))
        {
            return RoomController.instance.FindRoom(X, Y+1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y-1))
        {
            return RoomController.instance.FindRoom(X, Y-1);
        }
        return null;
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            if (trees != null)
            {
               trees.SetActive(false);
            }
        }
    }
    public void ActivarSpawn()
    {
        int RandomNumer = UnityEngine.Random.Range(0, 3);
        if(RandomNumer == 1 || RandomNumer == 0)
        {
            if(itemSpawner != null)
            {
                //itemSpawner.enabled = true;
                itemSpawner.SetActive(true);
            }
            //itemSpawner.enabled = true;
        }

    }
}
