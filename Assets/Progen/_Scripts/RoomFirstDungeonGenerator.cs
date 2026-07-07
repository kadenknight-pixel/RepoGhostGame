using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    // [SerializeField]
    // private bool randomWalkRooms = false;
    [SerializeField]
    private GameObject roomStarage;
    [SerializeField]
    private GameObject room;

    private List<GameObject> rooms = new();

    protected override void RunProceduralGeneration()
    {
        while (roomStarage.transform.childCount > 0)
            foreach (Transform child in roomStarage.transform)
            {
                GameObject.DestroyImmediate(child.gameObject);
            }
        rooms = new();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        
        floor = CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        // tilemapVisualizer.PaintFloorTiles(hall);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }


    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }else if(destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        HashSet<Vector2> roomCenters = new HashSet<Vector2>();
        var startPos  = new Vector3Int(0, 0, 100);
        foreach (var room in roomsList)
        {   
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                    if (col == offset && row == offset)
                    {
                        startPos = new Vector3Int(position.x, position.y, 0);
                    }
                }
            }
            CreateRoomBounds(startPos, room.size);
            startPos = new Vector3Int(0, 0, 100);
        }
        room.transform.position += new Vector3(10000, 10000, 0);
        return floor;
        
    }

    private void CreateRoomBounds(Vector3Int position, Vector3Int size)
    {
        room.transform.localScale = size - new Vector3((offset * 2) - 2, (offset * 2) - 2,0);
        // print(room.transform.localScale);
        room.transform.position = position + new Vector3(
            (room.transform.localScale.x / 2) - 3, (room.transform.localScale.y / 2) - 3, 0
        );
        room.name = rooms.Count.ToString();
        Instantiate(room, roomStarage.transform);
        rooms.Add(room);

    }
}
