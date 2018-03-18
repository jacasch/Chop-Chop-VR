using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularWoodLog : WoodLog {

    public override void ChopSplit() {
        int score = beingCut ? 50 : 100;
        Score.Split(transform.position + Vector3.up * 0.6f);
    }
}
