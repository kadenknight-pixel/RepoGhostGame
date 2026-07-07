using UnityEngine;

public class ElevatorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject roomStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = roomStorage.transform.GetChild(roomStorage.transform.childCount -1).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
