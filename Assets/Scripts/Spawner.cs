using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject obj;
    public float timeBetweenSpawns;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		if (timer > timeBetweenSpawns)
        {
            timer = 0.0f;
            Instantiate(obj, transform.position, Quaternion.identity);
        }
	}
}
