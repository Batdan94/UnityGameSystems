using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BoidStats>() != null && !other.GetComponent<BoidStats>().squished)
        {
            other.GetComponent<BoidStats>().StartCoroutine(other.GetComponent<BoidStats>().OnFire(other.gameObject));
        }
    }

}
