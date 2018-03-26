using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameIput : MonoBehaviour {
    [SerializeField]
    private string playerName;

    private void Update()
    {
        this.playerName = GetComponentInChildren<InputField>().transform.Find("Text").GetComponent<Text>().text;
    }

    private void OnEnable()
    {
        Reset();
    }

    public void UpdateName() {
    }

    public void Submit() {
        if (playerName.Length < 3)
        {
            return;
        }
        GameObject.FindObjectOfType<GameController>().SetPlayerName(playerName);
    }

    public void Reset()
    {
        playerName = "";
        GetComponentInChildren<InputField>().transform.Find("Text").GetComponent<Text>().text = "";
    }
}
