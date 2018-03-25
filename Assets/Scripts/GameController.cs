using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private enum Gamestate { Start, Intro, Running, GameOver };
    [SerializeField]
    private Gamestate state = Gamestate.Intro;
    private string currentPlayerName = "";
    private bool newStateFrame = true;
    private float elapsedStatetime = 0f;
    private GameDisplay display;

    //Mono Funcitons
    private void Start()
    {
        display = GetComponentInChildren<GameDisplay>();
    }
    private void Update()
    {
        elapsedStatetime += Time.deltaTime;
        //execute the right game state Function
        switch (state)
        {
            case Gamestate.Start:
                if (newStateFrame) {
                    StartStart();
                    newStateFrame = false;
                }
                UpdateStart();
                break;
            case Gamestate.Intro:
                if (newStateFrame)
                {
                    StartIntro();
                    newStateFrame = false;
                }
                UpdateIntro();
                break;
            case Gamestate.Running:
                if (newStateFrame)
                {
                    StartRunning();
                    newStateFrame = false;
                }
                UpdateRunning();
                break;
            case Gamestate.GameOver:
                if (newStateFrame)
                {
                    StartGameOver();
                    newStateFrame = false;
                }
                UpdateGameOver();
                break;
            default:
                print("Undefined Game State");
                break;
        }
    }

    private void ChangeState(Gamestate state){
        this.state = state;
        newStateFrame = true;
        elapsedStatetime = 0f;
    }


    //Game State Updates
    #region Start
    private bool playerReady;

    private void StartStart()
    {
        GetComponent<LogSpawner>().SpawnPlayerReadyLog();
        playerReady = false;
        string startmessage = "Um zu Beginnen,\nspalte das Holz\nmit der Axt!";
        display.SetText(startmessage);
    }
    private void UpdateStart()
    {
        //if player name is entered show wood to split
        //if wood is split go to next scene
    }

    public void PlayerIsReady() {
        ChangeState(Gamestate.Intro);
    }
    #endregion

    #region Intro
    //countdown to start the game



    static readonly string[] achtungFertigLos = new string[] { "Achtung", "Fertig", "Los" };
    const float countdownInterval = 1f;
    float lastCountdownTime;
    int countdownIndex = 0;
    private void StartIntro()
    {
        lastCountdownTime = Time.time;
        countdownIndex = 0;
        string startmessage = "Auf los gehts los!";
        display.SetText(startmessage);

        //start counting down
        Countdown();

    }
    private void UpdateIntro()
    {
        if (countdownIndex >= achtungFertigLos.Length)
        {
            //start the game
            ChangeState(Gamestate.Running);
            return;
        }

        if (lastCountdownTime + countdownInterval <= Time.time)
        {
            //do next interval
            Countdown();
        }
    }

    private void Countdown() {
        FloatingText.PrintInfrontOfPlayer(achtungFertigLos[countdownIndex], 1f, 0.01f, Color.white);
        countdownIndex++;
        lastCountdownTime = Time.time;
    }
    #endregion

    #region Running

    const string text = "Zeit {0}\nPunkte {1}";
    string score;
    const float playTime = 20f;
    private void StartRunning()
    {
        GetComponent<LogSpawner>().StartSpawning();
        string scoreText = "Punkte:" + Score.CurrentScore.ToString() ;
        display.ClearText();
    }
    private void UpdateRunning()
    {
        if (elapsedStatetime > playTime) {
            ChangeState(Gamestate.GameOver);
            return;
        }
        updateScoreDisplay();
        //update score
    }

    private void updateScoreDisplay() {
        score = Score.CurrentScore.ToString();
        display.SetText(string.Format(text, Mathf.Floor((playTime - elapsedStatetime) + 1).ToString(), Score.CurrentScore));
    }
    #endregion

    #region GameOver
    private const float gameOverStateTimeout = 30f;

    private void StartGameOver()
    {
        //Disable Log Spawning
        GetComponent<LogSpawner>().StopSpawning();
        //show score and ScoreBoard
        display.SetText("Deine Punkte:\n" + Score.CurrentScore.ToString());
        SaveScore();
    }
    private void UpdateGameOver()
    {
        //if player entered name go to startmenu and save score;
        if (elapsedStatetime >= gameOverStateTimeout) {
            RestartGame();
        }
    }

    private void SaveScore() {
        Score.Submit(currentPlayerName);
    }
    #endregion


    //Public Methods
    public void RestartGame() {
        ChangeState(Gamestate.Start);
    }    
}
