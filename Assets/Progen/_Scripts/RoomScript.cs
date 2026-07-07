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

    public List<Collision2D> triggerList = new()

    float difficulty = 0.05f;

    void PickRoomCharacter()
    {
        if (name == "0(Clone)")
        {
            roomCharacter = "Passive";
        }
        else if ( Random.value >= 0.5)
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
        // spawnpoints.Add(new Vector3Int(0,0,0));
        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                if (Random.value < difficulty)
                    spawnpoints.Add(new Vector3Int(y,x,0));
            }
        }

        foreach (var pos in spawnpoints)
        {
            enemy.transform.position = transform.position - new Vector3(32.65f,20.25f,0) + pos - new Vector3(roomHeight/2, roomWidth/2, 0);
            if (roomHeight % 2 != 0)
                enemy.transform.position += new Vector3(-0.5f, 0,0);
            if (roomWidth % 2 != 0)
                enemy.transform.position += new Vector3(0, -0.5f,0);
            Instantiate(enemy, enemyContainer.transform);
            
        }
    }


    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "bodyCollision")
        {
            
        }
            
            

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // print(collision.GetComponents());
        triggerList.Add(collision);
        

    }

    void Start()
    {
        PickRoomCharacter();
        // SpawnEnemies();
        if (roomCharacter == "Hostile")
        {
            SpawnEnemies();
        }
    }
}
