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
    Room lastlastRoom;
    Room auxRoom;

    public static bool boosDoor = false;


    GameObject player; 

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;

    bool updatedRooms = false;

    int contPuzzle = 0;

    public float maxHealth=0;
    public float leftHealth = 0;
    public bool loHealth = false;
    public bool fheal = false;
    public bool healing = false;
    public Vector2 posHealer;
    //ItemSpawner spawn;

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

        //ItemSpawner spawn = currRom.GetComponentInChildren<ItemSpawner>();
        //spawn = currRom.GetComponent<ItemSpawner>();
        //spawn.SpawObject();

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

            if (player.GetComponent<PlayerMovementInputSystem>().nivel == 0)
            {

                LoadRoom("End", tempRoom.X, tempRoom.Y);
            }
            else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 1)
            {
                LoadRoom("End2", tempRoom.X, tempRoom.Y);
            }
            else if (player.GetComponent<PlayerMovementInputSystem>().nivel >= 2)
            {
                LoadRoom("End3", tempRoom.X, tempRoom.Y);
            }


           
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
        currRom = loadedRooms[0];//Esta Linea puede dar problemas
    }
    public void RegisterRoom(Room room)
    {
        //Va MAL hay a veces que se abren todas las puertas y a veces que no
        if (currentLoadRoomData != null)
        {
            if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
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
        player = GameObject.FindGameObjectWithTag("Player");
        int randomAux = Random.Range(0, 2);
        string[] possibleRooms = new string[2]; 
        string[] puzzleRooms = new string[1]; 
        if (player.GetComponent<PlayerMovementInputSystem>().nivel == 0)
        {
            possibleRooms = new string[]
            {
                    //"Empty",
                    "Basic",
                    //"Basic_1",
                    //"Basic_2",
                    //"Basic_3",
                    //"Basic_4",
                    //"Basic_5",

                    //"Basic_17"
            };
                    
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 1)
        {
            possibleRooms = new string[]
            {
                "Basic1_1",
            };
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel >= 2)
        {
            possibleRooms = new string[]
            {
                //"Empty",
                //"Basic1",
                "Basic3",
            };
        }
        if (player.GetComponent<PlayerMovementInputSystem>().nivel == 0)
        {
            puzzleRooms = new string[]
            {

                //"King1",
                //"SkullPuzzle",
                //"RapidoQueSeQueman",
                "Ardilla",
                //"RapidoQueSeQueman 2",
            };

        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel == 1)
        {
            puzzleRooms = new string[]
            {

                "King2",
                "SkullPuzzle1",
                "RapidoQueSeQueman 1",
                "Ardilla1",

            };
        }
        else if (player.GetComponent<PlayerMovementInputSystem>().nivel >= 2)
        {
            puzzleRooms = new string[]
            {
                
                "King3",
                "SkullPuzzle2",
                "RapidoQueSeQueman 2",
                "Ardilla2",
            };
        }
        //string[] puzzleRooms = new string[]
        //{

        //    /*"King1",
        //    "SkullPuzzle",
        //    "RapidoQueSeQueman",*/
        //    "Ardilla",
        //    //"RapidoQueSeQueman 1",
        //    //"RapidoQueSeQueman 2",
        //};
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
        lastlastRoom = lastRoom;
        lastRoom = currRom;
        currRom = room;
        

        //los enemigos se quden quietos cuando la camara no este en la sala
        sumHealth();
        //UpdateRooms();
        StartCoroutine(RoomCoroutine());

        
        //currRom.ActivarSpawn();
    }

    public void sumHealth()
    {
        maxHealth= 0;
        foreach (Room room in loadedRooms)
        {
            if(currRom==room)
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                foreach (Enemy enemy in enemies)
                {
                    maxHealth += enemy.life;
                    
                    //Debug.Log("Not in Room");
                }
            }
        }
    }
    public float lHealth()
    {
        leftHealth = 0;
        foreach (Room room in loadedRooms)
        {
            if (currRom == room)
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                foreach (Enemy enemy in enemies)
                {
                    leftHealth += enemy.life;
                    //Debug.Log("Not in Room");
                }
            }
        }
        return leftHealth;
    }
    public bool lowHealth()
    {
        if (lHealth()< maxHealth * 0.3)
        {
            Debug.Log("Queda poca vida");
            loHealth = true;
        }
        else
        {
            loHealth= false;
        }
        
        return loHealth;
    }

    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRooms();
    }


    public void UpdateRooms()
    {
        boosDoor = false;
        foreach (Room room in loadedRooms)
        {
            if (currRom != room && lastRoom!=room && lastlastRoom!=room)
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                if(enemies != null)
                {
                    foreach(Enemy enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        //Debug.Log("Not in Room");
                    }

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
            else if (lastlastRoom == room)
            {

                foreach (Door door in room.GetComponentsInChildren<Door>())
                {

                    door.doorCollider.SetActive(false);
                }
            }

            else
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                //if (enemies != null)
                if (enemies.Length > 0)
                {
                    foreach (Enemy enemy in enemies)
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
                    Debug.Log("TERMINASTE LA RUN");
                    if(player != null && spawnedBossRoom)
                    {
                        
                        player.GetComponent<PlayerMovementInputSystem>().nivel++;
                        if (player.GetComponent<PlayerMovementInputSystem>().nivel < 3)
                        {
                            SceneManager.LoadScene("LoandingBoss");
                        }
                        else
                        {
                            SceneManager.LoadScene("Victory");
                        }
                        
                        //GameObject menuOpcion = GameObject.Find("Opcion");
                        //menuOpcion.transform.GetChild(0).gameObject.SetActive(true);

                    }

                }else{
                    
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                        room.GetComponent<Collider2D>().enabled = false;
                        fheal = false;
                        loHealth= false;
                        player.GetComponent<KnightScript>().bleed = 1;
                        //spawn.SpawObject();
                    }

                    //
                    currRom.ActivarSpawn();
                }
                
            }
        }
    }
}
