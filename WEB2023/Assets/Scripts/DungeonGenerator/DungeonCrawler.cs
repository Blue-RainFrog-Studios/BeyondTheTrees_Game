using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{
    public Direction last = Direction.top;
    public Direction last1 = Direction.top;
    public Vector2Int Position { get; set; }
    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, 3);
        if (toMove == Direction.right && last == Direction.left)
        {
            toMove = (Direction)Random.Range(0, 1);
            Position += directionMovementMap[toMove];
            last1 = last;
            last = toMove;

        }
        else if (toMove == Direction.left && last == Direction.right)
        {
            toMove = (Direction)Random.Range(1, 2);
            Position += directionMovementMap[toMove];
            last1 = last;
            last = toMove;

        }
        else if (last == Direction.top && last1 == Direction.left)
        {
            toMove = (Direction)Random.Range(0, 1);
            Position += directionMovementMap[toMove];
            last1 = last;
            last = toMove;

        }
        else if (last == Direction.top && last1 == Direction.right)
        {
            toMove = (Direction)Random.Range(1, 2);
            Position += directionMovementMap[toMove];
            last1 = last;
            last = toMove;

        }
        else
        {
            Position += directionMovementMap[toMove];
            last1 = last;
            last = toMove;

        }

        return Position;
    }
}