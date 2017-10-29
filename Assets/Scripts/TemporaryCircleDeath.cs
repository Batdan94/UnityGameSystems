using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCircleDeath : MonoBehaviour {

    public GameObject Circle;

    public GameObject fist;

	public GameObject threat;

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

    void circleOnPoint(Vector3 point)
    {
        Circle.transform.position = point + new Vector3(0.0f, 0.1f, 0.0f);
    }

    void attack()
    {
        var fistInstance = Instantiate(fist, new Vector3(Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity);
        if (fistInstance.GetComponent<FistController>() != null)
        {
            fistInstance.GetComponent<FistController>().circle = Circle;
        }

        if (fistInstance.GetComponent<LightningController>() != null)
        {
            fistInstance.GetComponent<LightningController>().circle = Circle;
        }
        Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
        
    }
}
