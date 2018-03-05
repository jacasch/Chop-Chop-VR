using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        /*Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time) * 10;
        transform.position = pos;*/

        Vector3 rot = transform.rotation.eulerAngles;
        rot.x += Time.deltaTime * 10;
        rot.x %= 360;
        transform.rotation = Quaternion.Euler(rot);
    }
}
