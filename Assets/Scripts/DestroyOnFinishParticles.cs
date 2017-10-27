using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinishParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(gameObject);
        }

	}
}
