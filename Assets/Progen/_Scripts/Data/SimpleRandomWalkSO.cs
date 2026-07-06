using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParamaters_" , menuName = "PCG/SimpleRandomWalkData")]


public class SimpleRandomWalkSO : ScriptableObject
{
    public int interations = 10, walklength = 10;
    public bool startRandomlyEachInteration = true;

}
