using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RandomizeParticle : MonoBehaviour {

    ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        ps.playbackSpeed *= Random.Range(0.9f, 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
