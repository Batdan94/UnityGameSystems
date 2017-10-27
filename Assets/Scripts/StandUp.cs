using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour {

    Rigidbody rb;
    public bool jumping = false;
    public bool grounded;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Detect if they're not standing straight
        if (Vector3.Dot(Vector3.up, transform.up) <= 0.2f && (!GetComponent<BoidStats>().squished))
        {
            
            //if ((transform.position.y > transform.localScale.y / 2) && !jumping)
            //{
            //    rb.AddForce(new Vector3(0.0f, -0.1f, 0.0f));
            //}
            if ((transform.position.y <= transform.localScale.y / 2))
            {
				rb.AddForce(Vector3.up * 1000 * Utils.Map(GetComponent<BoidStats>().size, 1.0f, 10.0f, 1.0f, 2.0f));
                jumping = true;
            }
        }
        if (Vector3.Dot(Vector3.up, transform.up) < 1.0f && !GetComponent<BoidStats>().squished)
        {
            //rb.constraints = RigidbodyConstraints.None;
            if (jumping)
            {
				Vector3 Euler = transform.localEulerAngles;
				Euler.x = 0.0f;
				Euler.z = 0.0f;
				rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(Euler), 0.1f));
            }
        }
        else
        {
            jumping = false;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.other.gameObject.layer == SortingLayer.NameToID("Floor"))
        {
            grounded = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.other.gameObject.layer == SortingLayer.NameToID("Floor"))
        {
            grounded = false;
        }
    }
}
