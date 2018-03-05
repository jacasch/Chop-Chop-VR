using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ChopHit() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Axe") {
            Vector3 axeSpeed = collision.gameObject.GetComponent<Axe>().velocity;
        }
    }
}
