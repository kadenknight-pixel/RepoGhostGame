using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    private int interations = 10;
    [SerializeField]
    public int walklength = 10;
    [SerializeField]
    public bool startRandomlyEachInteration = true;

    [SerializeField]
    private TileMapVisualiser TileMapVisualiser;

    public void RunProcedualGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        TileMapVisualiser.Clear();
        TileMapVisualiser.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < interations; i++)
        {
            var path = ProcedualGenerationAlgorithms.SimpleRandomWalk(currentPosition, walklength);
            floorPositions.UnionWith(path);
            if(startRandomlyEachInteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
