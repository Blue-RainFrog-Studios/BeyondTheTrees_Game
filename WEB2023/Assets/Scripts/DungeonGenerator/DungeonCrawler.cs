using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }
    public Vector2Int LastPosition { get; set; }
    public DungeonCrawler(Vector2Int startPos) {
        Position = startPos;
        LastPosition = startPos;
    }

    //public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    //{
    //    Direction toMove= (Direction)Random.Range(0, directionMovementMap.Count);
    //    Position += directionMovementMap[toMove];
    //    return Position;
    //}


    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        List<Direction> availableDirections = new List<Direction>(directionMovementMap.Keys);
        Direction toMove;

        // Mezcla aleatoriamente las direcciones disponibles
        ShuffleDirections(availableDirections);

        foreach (Direction direction in availableDirections)
        {
            if (directionMovementMap[direction] != LastPosition)
            {
                toMove = direction;
                Position += directionMovementMap[toMove];
                LastPosition = Position;
                return Position;
            }
        }

        // Si no se encontró una dirección válida, no se mueve
        return Position;
    }

    // Función para mezclar aleatoriamente una lista de direcciones
    private void ShuffleDirections(List<Direction> directions)
    {
        for (int i = 0; i < directions.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, directions.Count);
            Direction temp = directions[i];
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }
    }
}
