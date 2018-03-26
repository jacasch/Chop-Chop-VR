using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenManager : MonoBehaviour {
    private GameObject UIScoreBoard;
    private GameObject UINameInput;
    private GameObject UIScore;
    private GameObject UICountdown;

    private void Start()
    {
        UIScoreBoard = transform.Find("ScoreBoard").gameObject;
        UINameInput = transform.Find("NameInput").gameObject;
        UIScore = transform.Find("Score").gameObject;
        UICountdown = transform.Find("Countdown").gameObject;
    }

    public void ShowScoreBoard(bool visible) {
        UIScoreBoard.SetActive(visible);

    }
    public void ShowNameInputField(bool visible) {
        UINameInput.SetActive(visible);

    }
    public void ShowVRStream(bool visible) {
        int cameraDepth = visible ? 1 : -1;
        Camera.main.depth = cameraDepth;
        UIScore.SetActive(visible);
        UICountdown.SetActive(visible);
    }

    public void SetVisibilities(bool scoreBoardVisibility, bool nameInputVisibility, bool vrStreamVisibility)
    {
        ShowScoreBoard(scoreBoardVisibility);
        ShowNameInputField(nameInputVisibility);
        ShowVRStream(vrStreamVisibility);
    }
}
