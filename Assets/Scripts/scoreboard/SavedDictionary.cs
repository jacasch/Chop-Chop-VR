using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedDictionary{
    Dictionary<string, Dictionary<string, int>> playerScores;
    string fileName = "scoreboard.bin";
    string dictPath { get { return Application.dataPath + "/" + fileName; } }
}
