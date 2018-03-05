using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Axe : MonoBehaviour {
    //Velocity stuff
    private Vector3 lastFramePos;
    public Vector3 velocity;
    public float tiltAngle;

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
        UpdateTiltAngle();
	}

    void UpdatVelocity() {
        Vector3 frameMovementDistance = transform.position - lastFramePos;
        velocity = frameMovementDistance / Time.deltaTime;
        lastFramePos = transform.position;
    }

    void UpdateTiltAngle() {
        //tweak this according to model
        Vector3 bladeNormal = transform.parent.forward;
        Vector3 projectedVelocityY = Vector3.ProjectOnPlane(velocity, bladeNormal);
        float tiltOnBladeAxis = Vector3.Angle(velocity, projectedVelocityY); //calculates the rotation along the blade edge

        float tiltInAllDirections = Vector3.Angle(velocity, transform.parent.right); //calculates the tilt based on the pointing direction of the blade

        tiltAngle = tiltInAllDirections;
    }
}
