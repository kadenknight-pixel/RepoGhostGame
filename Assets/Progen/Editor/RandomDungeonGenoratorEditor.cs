using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]

public class RandomDungeonGenoratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator) target;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}



