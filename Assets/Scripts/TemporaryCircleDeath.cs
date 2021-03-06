﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCircleDeath : MonoBehaviour {

    public GameObject Circle;

    public GameObject fist;

	public GameObject threat;

    public GameObject uIManager;

    public bool game = true;
    public bool hasSmashed = false;
    public bool hasStruck = false;
	public bool hasLit = false;
    public bool hasPoisoned = false;

    public int selectedAttack = 0;  
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (game)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //var select = GameObject.FindWithTag("select").transform;
            var layerMask = (1 << 9);
            //layerMask = ~layerMask;
            if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
            {
                Circle.SetActive(true);
                circleOnPoint(hit.point);
                if (Input.GetMouseButtonDown(0))
                {
                    RobotZombieBehaviour.Instance.SetHasAttacked(true);
                    attack();
                }
            }
            else
            {
                Circle.SetActive(false);
            }
        }
    }

    void circleOnPoint(Vector3 point)
    {
        Circle.transform.position = point + new Vector3(0.0f, 0.1f, 0.0f);
    }

    public void attack()
    {
		switch (selectedAttack) {
		case 0:
			if (!hasSmashed) {
				var fistInstance = Instantiate (fist, new Vector3 (Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity);
				if (fistInstance.GetComponent<FistController> () != null) {
					fistInstance.GetComponent<FistController> ().circle = Circle;
					StartCoroutine (uIManager.GetComponent<UIManager> ().buttonCooldown (uIManager.GetComponent<UIManager> ().fistButton));
					Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
				}
			}
			break;
		case 1:
			if (!hasStruck) {
				var fistInstance = Instantiate (fist, new Vector3 (Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity);
				if (fistInstance.GetComponent<LightningController> () != null) {
					fistInstance.GetComponent<LightningController> ().circle = Circle;
					StartCoroutine (uIManager.GetComponent<UIManager> ().buttonCooldown (uIManager.GetComponent<UIManager> ().lightningButton));
					Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
				}
			}
			break;
		case 2:
			if (!hasLit) {
				var fistInstance = Instantiate (fist, new Vector3 (Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity);
				if (fistInstance.GetComponent<FireController> () != null) {
					fistInstance.GetComponent<FireController> ().circle = Circle;
					StartCoroutine (uIManager.GetComponent<UIManager> ().buttonCooldown (uIManager.GetComponent<UIManager> ().fireButton));
					Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
				}
			}
			break;
		case 3:
			if (!hasPoisoned) {
				var fistInstance = Instantiate (fist, new Vector3 (Circle.transform.position.x, 1.0f, Circle.transform.position.z), Quaternion.identity);
				if (fistInstance.GetComponent<PlagueController> () != null) {
					fistInstance.GetComponent<PlagueController> ().circle = Circle;
					StartCoroutine (uIManager.GetComponent<UIManager> ().buttonCooldown (uIManager.GetComponent<UIManager> ().plagueButton));
					Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
				}
			}
			break;
		}
    }

    public void attackExp()
    {
        var fistInstance = Instantiate(fist, new Vector3(Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity, Circle.transform);
        if (fistInstance.GetComponent<FistController>() != null)
        {
            fistInstance.GetComponent<FistController>().circle = Circle;
            fistInstance.GetComponent<FistController>().game = false;
        }

        if (fistInstance.GetComponent<LightningController>() != null)
        {
            fistInstance.GetComponent<LightningController>().game = false;
            fistInstance.GetComponent<LightningController>().circle = Circle;
            fistInstance.GetComponent<LightningController>().game = false;

        }
        if (fistInstance.GetComponent<FireController>() != null)
        {
            fistInstance.GetComponent<FireController>().circle = Circle;
        }
        if (fistInstance.GetComponent<PlagueController>() != null)
        {
            fistInstance.GetComponent<PlagueController>().circle = Circle;
            fistInstance.transform.position = new Vector3(Circle.transform.position.x, 1.0f, Circle.transform.position.z);
        }
        //Instantiate(threat, new Vector3(Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);

    }
}
