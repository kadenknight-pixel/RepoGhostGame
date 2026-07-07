using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject roomStorage;
    [SerializeField]
    private GameObject player;
    private List<GameObject> rooms = new();

    [SerializeField]
    private GameObject startRoom;

    [SerializeField]
    private GameObject endRoom;

    public GameObject[] allChildren;

    void Start()
    {
        player.transform.position = roomStorage.transform.GetChild(0).transform.position;
    }

    private Vector3 RandomRoom()
    {
        allChildren = new GameObject[roomStorage.transform.childCount];
        for (int i = 0; i < allChildren.Length; i++)
        {
            allChildren[i] = roomStorage.transform.GetChild(i).gameObject;
        }
        int choice = Random.Range( 0, allChildren.Length);
        return allChildren[choice].transform.position;
    }

    void Update()
    {
        
    }
}
