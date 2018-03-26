using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveScore : MonoBehaviour {
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Use this for initialization
    void Update () {
        scoreText.text = Score.CurrentScore.ToString();
	}
}
