using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class floatUpText : MonoBehaviour {
    public Color color = Color.white;

    public float upVelocity = 2;
    private float modifiedUpVelocity;
    public float lifetime = 1;
    private float startTime;

    private TextMesh tm;

	// Use this for initialization
	void Start () {
        tm = GetComponent<TextMesh>();
        startTime = Time.time;
        modifiedUpVelocity = upVelocity;
	}
	
	// Update is called once per frame
	void Update () {
        //Check death
        if (Time.time > startTime + lifetime) {
            Destroy(gameObject);
        }

        //lerp color
        float timeLerp = Mathf.Lerp(1, 0, (Time.time - startTime) / lifetime);
        color.a = timeLerp;
        tm.color = color;
        //adjust speed
        modifiedUpVelocity = upVelocity * timeLerp;

        //move up
        Vector3 position = transform.position;
        position.y += modifiedUpVelocity * Time.deltaTime;
        transform.position = position;

        FaceMainCamera();
    }

    private void FaceMainCamera() {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }
}
