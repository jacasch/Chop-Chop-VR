using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatingText {
    private static GameObject floatUpText = (GameObject)Resources.Load("prefabs/FloatUpText", typeof(GameObject));

    public static void Print(string text, Vector3 position) {
        GameObject temp = MonoBehaviour.Instantiate(floatUpText, position, Quaternion.identity);
        temp.GetComponent<TextMesh>().text = text;
    }
    public static void Print(string text, Vector3 position, float fontSize, Color color)
    {
        GameObject temp = MonoBehaviour.Instantiate(floatUpText, position, Quaternion.identity);
        temp.GetComponent<TextMesh>().text = text;
        temp.GetComponent<TextMesh>().characterSize = fontSize;
        temp.GetComponent<floatUpText>().color = color;
    }

    public static void Print(string text, Vector3 position, float fontSize, Color color, float floatUpSpeed, float textLifetime)
    {
        GameObject temp = MonoBehaviour.Instantiate(floatUpText, position, Quaternion.identity);
        temp.GetComponent<TextMesh>().text = text;
        temp.GetComponent<TextMesh>().characterSize = fontSize;
        temp.GetComponent<floatUpText>().color = color;
        temp.GetComponent<floatUpText>().upVelocity = floatUpSpeed; ;
        temp.GetComponent<floatUpText>().lifetime = textLifetime;
    }
}
