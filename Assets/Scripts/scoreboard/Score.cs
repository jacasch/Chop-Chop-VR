using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {
    private const int splitScore = 100;
    private const int cutScore = 50;
    private const int birdHitPenaulty = -200;

    private static int splitCount = 0;
    private static float lastSplitTime = 0;
    private static float combotimeout = 1.5f;
    private static readonly int[] comboSteps = {1,1,2,2,5,5,10,10};
    private static int comboMultiplier = 1;
    private static Vector3 lastHitPos = Vector3.zero;

    private static int score = 0;
    public static int CurrentScore { get { return score; } }

    private static void resetCombo()
    {
        splitCount = 0;
        comboMultiplier = 1;
    }
    private static void CheckCombo() {
        if (splitCount <= 0) {
            //abort
            return;
        }

        if (splitCount > comboSteps.Length)
        {
            //start fury mode
            return;
        }


        comboMultiplier = comboSteps[splitCount];
        int lastMultiplier = comboSteps[splitCount - 1];
        if (comboMultiplier != lastMultiplier)
        {
            FloatingText.PrintInfrontOfPlayer("Combo X" + comboMultiplier.ToString(), 1.5f, 0.016f, Color.red);
        }
    }





    public static void Submit(string name)
    {
        Highscores.Add(name, score);
        resetCombo();
        score = 0;
    }

    public static void Split(Vector3 position)
    {
        splitCount++;
        lastSplitTime = Time.time;
        CheckCombo();
        int hitScore = splitScore * comboMultiplier;
        lastHitPos = position;
        AddScore(hitScore.ToString());
    }
    public static void Cut(Vector3 position)
    {
        resetCombo();
        CheckCombo();
        int hitScore = cutScore * comboMultiplier;
        lastHitPos = position;
        AddScore(hitScore.ToString());
    }
    public static void BirdHit(Vector3 position)
    {
        resetCombo();
    }






    private static void AddScore(string text)
    {
        FloatingText.Print(text, lastHitPos);
    }
    private static void AddScore(string text, Vector3 hitPosition) {
        FloatingText.Print(text, hitPosition);
    }
}
