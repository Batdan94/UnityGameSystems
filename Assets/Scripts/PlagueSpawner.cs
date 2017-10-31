using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueSpawner : MonoBehaviour {

    [SerializeField]
    GameObject Plague;

    int plagueCount = 0;
	
	void Update()
    {
        if (plagueCount == 0 && Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 10.0f, Input.mousePosition.z));
            Debug.Log(pos);
            Instantiate(Plague, new Vector3(pos.x, 0.0f, pos.z), Quaternion.identity);
            plagueCount++;
        }
    }
}
