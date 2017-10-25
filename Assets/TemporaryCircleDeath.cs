using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCircleDeath : MonoBehaviour {

    public GameObject Circle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var select = GameObject.FindWithTag("select").transform;
        var layerMask = (1 << 9);
        //layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
        {
            Circle.SetActive(true);
            circleOnPoint(hit.point);
        }
        else
        {
            Circle.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }

    void circleOnPoint(Vector3 point)
    {
        Circle.transform.position = point + new Vector3(0.0f, 0.1f, 0.0f);
    }

    void attack()
    {
        foreach(var boid in GameManager.Instance.BoidsManager.robotZombies)
        {
            if (Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z) , Circle.transform.position) < Circle.GetComponent<Circle>().xradius)
            {
                boid.GetComponent<BoidStats>().StartCoroutine(boid.GetComponent<BoidStats>().squash(boid));
                //boid.gameObject.SetActive(false);
            }
        }
    }
}
