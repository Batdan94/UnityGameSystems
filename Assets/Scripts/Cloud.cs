using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    Vector3 direction;
    Vector3 rotation;
    Timer timer;

	// Use this for initialization
	void Start () {
        direction = transform.position.x < transform.position.z ? -Vector3.forward : -Vector3.right;
        rotation = new Vector3(0.0f, Random.Range(0.0f, 0.3f), 0.0f);
        timer = new Timer(30.0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction * 0.1f;
        transform.Rotate(rotation);
        if (timer.Trigger())
        {
            Destroy(gameObject);
        }
	}
}
