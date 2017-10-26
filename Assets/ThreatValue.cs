using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatValue : MonoBehaviour {

	public float threat = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		threat -= Time.deltaTime;
		if (threat <= 0)
			Destroy (this.gameObject);
	}
}
