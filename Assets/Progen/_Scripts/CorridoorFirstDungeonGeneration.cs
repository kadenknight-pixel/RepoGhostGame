using System;
using System.Buffers.Text;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorridoorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridoorLength = 14, corridoorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float room_percent = 0.8f;
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;



    protected override void RunProcedualGeneration()
    {
        CorridoorFirstDungeonGeneration();
    }

    private void CorridoorFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        createCorridoors(floorPositions);
        
        tilemapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    private void createCorridoors(HashSet<Vector2Int> floorPositions)
    {
        var currentPosition = startPosition;

        for (int i = 0; i < corridoorCount; i++)
        {
            var corridoor = ProcedualGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridoorLength);
            currentPosition = corridoor[corridoor.Count - 1];
            floorPositions.UnionWith(corridoor);
        }
    }
}
