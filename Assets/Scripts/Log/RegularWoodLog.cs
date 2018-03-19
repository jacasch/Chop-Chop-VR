using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularWoodLog : WoodLog {

    public override void ChopSplit() {
        if (!beingCut)
        {
            Score.Split(transform.position + Vector3.up * 0.6f);
        }
        else {
            Score.Cut(transform.position + Vector3.up * 0.6f);
        }
    }
}
