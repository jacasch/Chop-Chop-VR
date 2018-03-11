using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(scoreboard))]
public class ScoreboardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        scoreboard myScript = (scoreboard)target;
        if (GUILayout.Button("Clear Scores"))
        {
            Highscores.Clear();
        }
        if (GUILayout.Button("Add Rabndom Score"))
        {
            Highscores.Add("test" + Random.Range(0, 100).ToString(), Random.Range(10, 10000));
        }
    }
}
