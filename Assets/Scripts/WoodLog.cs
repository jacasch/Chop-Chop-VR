using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLog : MonoBehaviour {

    public float cutThreshold;
    public float splitResitance;

    public GameObject logSplit;

    private bool beingCut = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChopHit() { }

    private void OnTriggerEnter(Collider collider)
    {
        print("collision");
        if (collider.gameObject.layer == LayerMask.NameToLayer("Blade")) {
            //collision with axe Blade
            float tiltAngle = collider.gameObject.GetComponent<Axe>().tiltAngle;
            Vector3 velocity = collider.gameObject.GetComponent<Axe>().velocity;

            //print(tiltAngle);

            CalculateHitImpact(collider.transform.parent, velocity, tiltAngle);
        }
    }

    private void CalculateHitImpact(Transform axe, Vector3 bladeVelocity, float bladeTiltAngle) {
        float impactAngle = Vector3.Angle(Vector3.down, bladeVelocity.normalized);

        if (bladeTiltAngle <= 30) {
            //blade is is not tiltet off os there could actually be a cut
            if (impactAngle <= 30) {
                //we are hitting the log from the top and not the sides or anything
                float bladeVelocityMadnitude = bladeVelocity.magnitude;
                print(bladeVelocityMadnitude);
                if (bladeVelocityMadnitude <= cutThreshold) {
                    //we are not Fast Enough
                    //HIT THE LOG
                } else
                {
                    //at least fast enough to hit the log
                    if (bladeVelocityMadnitude <= splitResitance)
                    {
                        //we have enough impact to cut into the wood, but not enough to split it
                        //CUT INTO Log
                    } else
                    {
                        //Impact is enough to split the log in half
                        SplitLog(axe);
                    }
                }
            }
        }
    }

    private void HitLog()
    {
        ////diable physics components of log
        //instantiate log rigidbody in position of log
        //apply force to instance
        //destroy gameopbject
    }
    private void CutLog(Transform axe, Vector3 impactVelocity)
    {
        beingCut = true;
        //set axe as parent
        transform.parent = axe.transform;
        //move log up the blade according to impact strength
        Vector3 localPos = transform.localPosition;
        localPos -= impactVelocity;
        splitResitance -= impactVelocity.magnitude;
        transform.localPosition = localPos;

    }
    private void SplitLog(Transform axe)
    {
        /*//diable physics components of log
        gameObject.GetComponent<Collider>().enabled = false;*/
        //instanitate 2 logsplits in position of log
        GameObject logInstance1 = Instantiate(logSplit, transform.position, transform.rotation, null);
        GameObject logInstance2 = Instantiate(logSplit, transform.position, transform.rotation, null);
        //rotate to blade
        logInstance1.transform.Rotate(0, 0, 90);
        logInstance2.transform.Rotate(0, 0, -90);
        //apply force to splits
        logInstance1.GetComponent<Rigidbody>().AddForceAtPosition(logInstance1.transform.forward * 20, logInstance1.transform.position + new Vector3(0, 0, 1));
        logInstance2.GetComponent<Rigidbody>().AddForceAtPosition(logInstance2.transform.forward * -20, logInstance2.transform.position + new Vector3(0, 0, 1));
        //slogInstance.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
        //destroy gameopbject
        Destroy(gameObject);
    }
}
