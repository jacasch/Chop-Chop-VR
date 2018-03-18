using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {
    private const int splitScore = 100;
    private const int cutScore = 50;
    private const int birdHitPenaulty = -200;
    private const int comboSteps = 2;

    private static int comboMultiplier = 1;
    private static int splitcount = 0;
    private static Vector3 lastHitPos = Vector3.zero;

    public static int score = 0;

    private static void resetCombo()
    {
        splitcount = 0;
        comboMultiplier = 1;
    }
    private static void CheckCombo() {
        int nextComboMUltiplier = Mathf.FloorToInt(splitcount / comboSteps) + 1;

        if (comboMultiplier != nextComboMUltiplier) {
            FloatingText.Print("Combo X" + comboMultiplier.ToString(), lastHitPos, 0.012f, Color.red);
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
        splitcount++;
        CheckCombo();
        int hitScore = splitScore * comboMultiplier;
        AddScore(hitScore.ToString());
    }
    public static void Cut(Vector3 position)
    {
        resetCombo();
        CheckCombo();
        int hitScore = splitScore * comboMultiplier;
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
