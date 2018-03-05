using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Axe : MonoBehaviour {
    //Velocity stuff
    private Vector3 lastFramePos;
    public Vector3 velocity;

    #region initialize components
    Collider col;
    Rigidbody rb;
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdatVelocity();
	}

    void UpdatVelocity() {
        Vector3 frameMovementDistance = transform.position - lastFramePos;
        velocity = frameMovementDistance / Time.deltaTime;
        lastFramePos = transform.position;
    }
}
