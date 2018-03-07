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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blade"))
        {
            print("bladeCollision");
            //collision with axe Blade
            float tiltAngle = collision.gameObject.GetComponent<Axe>().tiltAngle;
            Vector3 velocity = collision.gameObject.GetComponent<Axe>().velocity;

            //print(tiltAngle);

            CalculateHitImpact(collision.transform.parent, velocity, tiltAngle, collision);
        }
    }

    private void CalculateHitImpact(Transform axe, Vector3 bladeVelocity, float bladeTiltAngle, Collision collision) {
        float impactAngle = Vector3.Angle(Vector3.down, bladeVelocity.normalized);

        if (bladeTiltAngle <= 30) {
            //blade is is not tiltet off os there could actually be a cut
            if (impactAngle <= 30) {
                //we are hitting the log from the top and not the sides or anything
                float bladeVelocityMadnitude = bladeVelocity.magnitude;
                print(bladeVelocityMadnitude);
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

    private void HitLog(Vector3 collisionPoint, Vector3 impactForce)
    {
        gameObject.layer = LayerMask.NameToLayer("Environment");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForceAtPosition(impactForce * 500, collisionPoint);
    }
    private void CutLog(Transform axe, Vector3 impactVelocity)
    {
        beingCut = true;
        //set axe as parent
        transform.parent = axe.transform;
        //move log up the blade according to impact strength
        Vector3 position = transform.position;
        float logLength = GetComponent<Collider>().bounds.size.y;
        //position -= impactVelocity.normalized * (logLength / 2) * (impactVelocity.magnitude / splitResitance);
        position -= impactVelocity.normalized * 0.1f;
        splitResitance -= impactVelocity.magnitude;
        transform.position = position;

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
