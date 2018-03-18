﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLog : MonoBehaviour {

    public float cutThreshold;
    public float splitResitance;
    private float leftOverResistance;

    public GameObject logSplit;

    public bool beingCut = false;
    private bool alreadySplit = false;
    private float lastCollision = 0;

    private Vector3 cutNormalLocalSpace;

	// Use this for initialization
	void Start () {
        leftOverResistance = splitResitance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChopHit() { }
    public virtual void ChopCut() { }
    public virtual void ChopExtendCut() { }
    public virtual void ChopSplit() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blade"))
        {
            if (Time.time - lastCollision < 0.1f)
                return;
            lastCollision = Time.time;
            print("bladeCollision");
            //collision with axe Blade
            float tiltAngle = collision.gameObject.GetComponent<Axe>().tiltAngle;
            Vector3 velocity = collision.gameObject.GetComponent<Axe>().velocity;

            //print(tiltAngle);

            CalculateHitImpact(collision.transform.parent, velocity, tiltAngle, collision);
        }
        if (beingCut)
        {
            if (Time.time - lastCollision < 0.1f)
                return;
            lastCollision = Time.time;
            CalculateCutImpact(transform.parent.Find("Blade").GetComponent<Axe>().velocity);
        }
    }

    private void CalculateHitImpact(Transform axe, Vector3 bladeVelocity, float bladeTiltAngle, Collision collision) {
        float impactAngle = Vector3.Angle(Vector3.down, bladeVelocity.normalized);

        if (bladeTiltAngle <= 30) {
            //blade is is not tiltet off os there could actually be a cut
            if (impactAngle <= 30) {
                //we are hitting the log from the top and not the sides or anything
                float bladeVelocityMadnitude = bladeVelocity.magnitude;
                if (bladeVelocityMadnitude <= cutThreshold) {
                    //we are not Fast Enough
                } else
                {
                    //at least fast enough to hit the log
                    if (bladeVelocityMadnitude <= splitResitance)
                    {
                        //we have enough impact to cut into the wood, but not enough to split it
                        CutLog(axe, bladeVelocity);
                        return;
                    } else
                    {
                        //Impact is enough to split the log in half
                        SplitLog(axe);
                        return;
                    }
                }
            }
        }
        if (bladeVelocity.magnitude >= 1.3f)
        {
            HitLog(collision.contacts[0].point, bladeVelocity);
        }
    }

    private void CalculateCutImpact(Vector3 ImpactForce) {
        float AngleCutToImpact = Mathf.Abs(90 - Vector3.Angle(transform.localToWorldMatrix.MultiplyVector(cutNormalLocalSpace), ImpactForce));

        ExtendCut(ImpactForce);
        if (leftOverResistance <= 0) {
            SplitLog(transform.parent);
        }
    }

    private void HitLog(Vector3 collisionPoint, Vector3 impactForce)
    {
        print("log hit registered");
        gameObject.layer = LayerMask.NameToLayer("Environment");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForceAtPosition(impactForce * 500, collisionPoint);
        ChopHit(); ;
    }
    private void CutLog(Transform axe, Vector3 impactForce)
    {
        print("log cut registered");
        beingCut = true;
        cutNormalLocalSpace = transform.worldToLocalMatrix.MultiplyVector(axe.transform.forward.normalized);
        //set axe as parent
        transform.parent = axe.transform;
        //change collision layer to environment
        gameObject.layer = LayerMask.NameToLayer("Environment");
        //move log up the blade according to impact strength
        ExtendCut(impactForce);
        ChopCut();

    }
    private void ExtendCut(Vector3 force)
    {
        print("log cut extended");
        //Calculate new splitresistance
        leftOverResistance -= force.magnitude;
        //TODO Offset for relative blade placemnet on first cut
        float cutRatio = leftOverResistance / splitResitance;
        Transform blade = transform.parent.Find("Blade");
        float halfLogLength = GetComponent<Collider>().bounds.size.y / 2f;
        transform.position = blade.position - transform.forward * halfLogLength * cutRatio - transform.forward * halfLogLength;
        ChopExtendCut();
    }
    private void SplitLog(Transform axe)
    {
        print("log split registered");
        if (alreadySplit)
            return;
        alreadySplit = true;
        /*//diable physics components of log
        gameObject.GetComponent<Collider>().enabled = false;*/

        //rotate to blade
        Vector3 axeUpProj = axe.transform.up;
        axeUpProj.y = 0; //projection on xz plane (ground)
        Vector3 logFwdProj = transform.up;
        logFwdProj.y = 0; //projection on xz plane (ground)
        float angleAxeToLog = Vector3.Angle(axeUpProj, logFwdProj);
        transform.Rotate(0, 0, angleAxeToLog);
        //instanitate 2 logsplits in position of log
        GameObject logInstance1 = Instantiate(logSplit, transform.position, transform.rotation, null);
        GameObject logInstance2 = Instantiate(logSplit, transform.position, transform.rotation, null);
        //rotate splits
        logInstance1.transform.Rotate(0, 0, 0);
        logInstance2.transform.Rotate(0, 0, 180);
        //apply force to splits
        logInstance1.GetComponent<Rigidbody>().AddForceAtPosition(logInstance1.transform.forward * 20, logInstance1.transform.position + new Vector3(0, 0, 1));
        logInstance2.GetComponent<Rigidbody>().AddForceAtPosition(logInstance2.transform.forward * -20, logInstance2.transform.position + new Vector3(0, 0, 1));
        //slogInstance.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
        //destroy gameopbject
        ChopSplit();
        Destroy(gameObject);
    }
}
