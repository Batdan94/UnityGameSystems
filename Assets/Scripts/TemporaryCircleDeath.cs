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
        }
        else
        {
            Circle.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RobotZombieBehaviour.Instance.SetHasAttacked(true);
            attack();
        }
    }

    void circleOnPoint(Vector3 point)
    {
        Circle.transform.position = point + new Vector3(0.0f, 0.1f, 0.0f);
    }

    void attack()
    {
        var fistInstance = Instantiate(fist, new Vector3(Circle.transform.position.x, 10.0f, Circle.transform.position.z), Quaternion.identity);
		Instantiate (threat, new Vector3 (Circle.transform.position.x, 0.0f, Circle.transform.position.z), Quaternion.identity);
        StartCoroutine(raisingFist(fistInstance));
        foreach(var boid in GameManager.Instance.BoidsManager.robotZombies)
        {
            if (Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z) , Circle.transform.position) < Circle.GetComponent<Circle>().xradius)
            {
                boid.GetComponent<BoidStats>().StartCoroutine(boid.GetComponent<BoidStats>().squash(boid));
                    //boid.gameObject.SetActive(false);
            }
            else if (Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z), Circle.transform.position) > Circle.GetComponent<Circle>().xradius &&
                   Vector3.Distance(new Vector3(boid.transform.position.x, 0.0f, boid.transform.position.z), Circle.transform.position) < (Circle.GetComponent<Circle>().xradius * 2))
            {
                Debug.Log("Is within Blast radius");
                boid.GetComponent<Rigidbody>().AddExplosionForce(1000.0f, Circle.transform.position, Circle.GetComponent<Circle>().xradius * 2);
                boid.GetComponent<Rigidbody>().AddForce(new Vector3(0, 3000, 0));
            }
        }
    }

    IEnumerator raisingFist(GameObject fistinst)
    {
        while (fistinst.transform.position.y > 0.0f)
        {
            if ((fistinst.transform.position + Vector3.down * Time.deltaTime * 200).y < 0.0f)
            {
                fistinst.transform.position = new Vector3(fistinst.transform.position.x, 0.0f, fistinst.transform.position.z);
            }
            else
            {
                fistinst.transform.position = (fistinst.transform.position + Vector3.down * Time.deltaTime * 200);
            }
            yield return new WaitForFixedUpdate();
        }
        Timer timer = new Timer(2.0f);
        while (!timer.Trigger())
        {
            fistinst.transform.position += Vector3.up * Time.deltaTime * 5;
            yield return new WaitForFixedUpdate();
        }
        Destroy(fistinst);
        yield return null;
    }
}
