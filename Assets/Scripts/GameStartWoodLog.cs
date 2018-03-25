using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartWoodLog : WoodLog {
    public override void ChopSplit() {
        GameObject.FindObjectOfType<GameController>().PlayerIsReady();
    }

    public override void ChopHit() {
        GameObject.FindObjectOfType<LogSpawner>().SpawnPlayerReadyLog();
    }
}
