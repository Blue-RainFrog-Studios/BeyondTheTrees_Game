//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DungeonGenerator : MonoBehaviour
//{
//    public DungeonGenerationData dungeonGenerationData;

//    private List<Vector2Int> dungeonRooms;

//    private void Start()
//    {
//        dungeonRooms=DungeonCrawlercontroller.GenerateDungeon(dungeonGenerationData);
//        SpawnRooms(dungeonRooms);
//    }

//    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
//    {
//        RoomController.instance.LoadRoom("Start", 0, 0);
//        foreach(Vector2Int roomLocation in rooms)
//        {
//            //atencion aqui se hace para crear la room del boss, no es necesaria pero lo dejo por si queremos hacer rooms especiuales
//            //minuto 13 del video parte 5
//            if(roomLocation == dungeonRooms[dungeonRooms.Count - 1] && !(roomLocation == Vector2Int.zero))
//            {
//                RoomController.instance.LoadRoom("End", roomLocation.x, roomLocation.y);
//            }
//            else
//            {
//                RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
//            }

//        }
//    }

//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    public string[] EmptyRooms = { "Empty", "Shop" }; //get random string room from this array of strings c#

    


    private void Start()
    {
        dungeonRooms = DungeonCrawlercontroller.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {

                RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y); //changed EmptyRooms.ToString() from Empty
                                                                                                                      //RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
                //Debug.Log(EmptyRooms.RandomItem());
                //Debug.Log(EmptyRooms.ToString());
            

        }
    }

}

public static class ArrayExtensions
{
    // This is an extension method. RandomItem() will now exist on all arrays.
    public static T RandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}