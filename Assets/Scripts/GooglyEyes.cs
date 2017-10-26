using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEyes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.value, Random.value , Random.value)* Random.Range(-1.0f, 1.0f));
	}
}
