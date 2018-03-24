using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private enum Gamestate { Start, Intro, Running, GameOver};
    private Gamestate state = Gamestate.Intro;
    private Gamestate lastState;

    //Mono Funcitons
    private void Start()
    {
        
    }
    private void Update()
    {
        //execute the right game state Function
        switch (state)
        {
            case Gamestate.Start:
                if (state != lastState) {
                    StartStart();
                }
                UpdateStart();
                break;
            case Gamestate.Intro:
                if (state != lastState)
                {
                    StartIntro();
                }
                UpdateIntro();
                break;
            case Gamestate.Running:
                if (state != lastState)
                {
                    StartRunning();
                }
                UpdateRunning();
                break;
            case Gamestate.GameOver:
                if (state != lastState)
                {
                    StartGameOver();
                }
                UpdateGameOver();
                break;
            default:
                print("Undefined Game State");
                break;
        }
        lastState = state;
    }


    //Game State Updates
    private void StartStart()
    {

    }
    private void UpdateStart()
    {

    }
    private void StartIntro()
    {

    }
    private void UpdateIntro()
    {

    }
    private void StartRunning()
    {

    }
    private void UpdateRunning()
    {

    }
    private void StartGameOver()
    {

    }
    private void UpdateGameOver()
    {

    }


    //Public Methods
    public void StartNewGame() {
        //spawn Initial Log
        //set 
    }
    
}
