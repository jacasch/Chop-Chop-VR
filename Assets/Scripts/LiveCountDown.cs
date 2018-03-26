using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveCountDown : MonoBehaviour {

    private Text countdownText;
    private GameController controller;

    private void Start()
    {
        countdownText = GetComponent<Text>();
        controller = GameObject.FindObjectOfType<GameController>();
    }

    // Use this for initialization
    void Update()
    {
        countdownText.text = TimeFormat.MinSec(controller.LeftPlayTime());
    }
}
