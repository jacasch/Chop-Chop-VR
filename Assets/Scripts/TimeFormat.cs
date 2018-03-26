using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeFormat {
    public static string MinSec(float seconds) {
        int min = Mathf.FloorToInt(seconds / 60);
        int sec = (int) seconds % 60;
        return min.ToString("00") + ":" + sec.ToString("00");
    }
}
