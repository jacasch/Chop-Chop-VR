using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class scoreboard : MonoBehaviour {
    Text scoreboardText;
    public string scores;

    private void Start() {
        scoreboardText = GetComponent<Text>();
        Highscores.Add("test", Random.Range(10,1000));
        //Highscores.Clear();
    }

    private void Update() {
        scoreboardText.text = Highscores.AsString(10);
    }
}
