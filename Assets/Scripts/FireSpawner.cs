using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour {

	[SerializeField]
	GameObject tempFire;



	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, 10.0f, Input.mousePosition.z));
			Debug.Log (pos);
			Instantiate (tempFire, new Vector3 (pos.x, 0.0f, pos.z), Quaternion.identity);
		}
	}
}
