using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using Random = UnityEngine.Random;


public class RoomScript : MonoBehaviour
{
    private string roomCharacter = "";
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyContainer;


    void PickRoomCharacter()
    {
        if (name == "0(Clone)")
        {
            roomCharacter = "Passive";
        }
        else if ( Random.value >= 0.0)
        {
            roomCharacter = "Hostile";
        }
        else if( Random.value > 0.8)
        {
            roomCharacter = "Passive";
        }
        else
        {
            roomCharacter = "Tresure";
        }
    }

    void SpawnEnemies()
    {
        int roomHeight = (int)transform.localScale.x - 2;
        int roomWidth = (int)transform.localScale.y - 2;
        // int potentialSpawnpoints = roomWidth * roomHeight;
        List<Vector3Int> spawnpoints = new();
        spawnpoints.Add(new Vector3Int(0,0,0));
        // for (int x = 0; x < roomWidth; x++)
        // {
        //     for (int y = 0; y < roomHeight; y++)
        //     {
        //         if (Random.value < 1.0)
        //             spawnpoints.Add(new Vector3Int(y,x,0));
        //     }
        // }

        foreach (var pos in spawnpoints)
        {
            enemy.transform.position = transform.position - new Vector3(32.65f,20.75f,0) + pos - new Vector3(roomHeight/2, roomWidth/2, 0);
            Instantiate(enemy, enemyContainer.transform);
            print(transform.position);
        }
    }




    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "bodyCollision")
            Debug.Log(roomCharacter);
            

    }
    void Start()
    {
        PickRoomCharacter();
        SpawnEnemies();
        // if (roomCharacter == "Hostile")
        // {
        //     SpawnEnemies();
        // }
    }
}
