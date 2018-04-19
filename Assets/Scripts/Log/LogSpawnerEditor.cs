#if (UNITY_EDITOR)
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LogSpawner))]
public class LogSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LogSpawner spawner = (LogSpawner)target;
        if (GUILayout.Button("Spawn Log"))
        {
            spawner.SpawnLog();
        }
    }
}
#endif