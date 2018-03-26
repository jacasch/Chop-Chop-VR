using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDisplay : MonoBehaviour {

    private TextMesh tm;

	// Use this for initialization
	void Start () {
        tm = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(string text)
    {
        tm.text = text;
    }

    public void ClearText() {
        tm.text = "";
    }
}
