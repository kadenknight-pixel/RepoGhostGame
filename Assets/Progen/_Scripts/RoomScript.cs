using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;



public class RoomScript : MonoBehaviour
{
    // private enum RoomScript
    // {
    //     Passive,
    //     Hostile,
    //     Tresure
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("ahhhhh");
    }

    void Start()
    {
        
    }
}
