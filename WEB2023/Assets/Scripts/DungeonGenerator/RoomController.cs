using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}


public class RoomController : MonoBehaviour
{
    public static bool iHaveFinishied = false;
    // Start is called before the first frame update
    public static RoomController instance;
    string currentWorldName = "Basement";

    int cont = 0;

    RoomInfo currentLoadRoomData;

    Room currRom;

    Room lastRoom;

    public static bool boosDoor = false;



    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;

    bool updatedRooms = false;

    int contPuzzle = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
    }

    void Update()
    {

        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());

            }else if(spawnedBossRoom && !updatedRooms) {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                UpdateRooms();
                if(cont == 1)
                {
                    updatedRooms = true;
                }
                
            }
            iHaveFinishied = true;
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {

            Room bossRoom = loadedRooms[loadedRooms.Count-1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove=loadedRooms.Single(r=>r.X==tempRoom.X && r.Y==tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End",tempRoom.X,tempRoom.Y);
        }

        

        cont= 1;

    }
    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);

    }
     public void DestroyRooms()
    {
        foreach (Room room in loadedRooms)
        {


            Destroy(room.gameObject);

        }
        loadedRooms.Clear();

        
    }
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }
    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X,currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height, 0);
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData + " " + room.X + "," + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;


            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRom = room;
            }

            loadedRooms.Add(room);
            //room.RemoveUnconnectedDoors();
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
    public string GetRandomRoomName()
    {
        int randomAux = Random.Range(0, 2);
        string[] possibleRooms = new string[]
        {
            //"Empty",
            "Basic3",           
        };
        string[] possibleRooms1 = new string[]
{
            //"Empty",
            "Basic1",
        };
        string[] puzzleRooms = new string[]
        {
            "King1",
            "SkullPuzzle",
            "RapidoQueSeQueman",
            "RapidoQueSeQueman 1",
            "RapidoQueSeQueman 2",
        };
        if(randomAux == 0 && contPuzzle < 1)
        {
            contPuzzle++;
            return puzzleRooms[Random.Range(0, puzzleRooms.Length)];
        }
        else
        {
            return possibleRooms[Random.Range(0, possibleRooms.Length)];
        }
        
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRom= room;
        lastRoom = currRom;
        currRom = room;


        //los enemigos se quden quietos cuando la camara no este en la sala

        //UpdateRooms();
        StartCoroutine(RoomCoroutine());
    }

    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRooms();
    }


    public void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currRom != room && lastRoom!=room)
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if(enemies != null)
                {
                    foreach(EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        //Debug.Log("Not in Room");
                    }
                    //related to cierre de puertas

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }


                }
                else 
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }else if (lastRoom == room)
            {

                foreach (Door door in room.GetComponentsInChildren<Door>())
                {

                    door.doorCollider.SetActive(true);
                }
            }

            else
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                //if (enemies != null)
                if (enemies.Length > 0)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        Debug.Log("In Room");
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }

                }else if(currRom== loadedRooms[loadedRooms.Count - 1] && enemies.Length == 0){
                    boosDoor = true;
                    //AQUI CAMBIAR ESCENA

                    

                }else{
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }

                }
            }
        }
    }
}
