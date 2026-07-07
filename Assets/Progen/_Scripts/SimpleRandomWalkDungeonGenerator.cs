using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParamaters;



    protected override void RunProcedualGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParamaters, startPosition);
        tilemapVisualiser.Clear();
        tilemapVisualiser.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO paramaters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < paramaters.interations; i++)
        {
            var path = ProcedualGenerationAlgorithms.SimpleRandomWalk(currentPosition, paramaters.walklength);
            floorPositions.UnionWith(path);
            if(paramaters.startRandomlyEachInteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}