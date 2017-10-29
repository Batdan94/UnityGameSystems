using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour {

    public List<GameObject> cloudsPrefab;
    public List<GameObject> cloudSpawners;
    public float cloudSpawnTime;
    Timer timer;
    // Use this for initialization
    void Start () {
        timer = new Timer(cloudSpawnTime);
        cloudSpawners = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cloudSpawners.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update () {
		if (timer.Trigger())
        {
            var cloud = Instantiate(cloudsPrefab[Random.Range(0, cloudsPrefab.Count)], RandomPointInsideOf(cloudSpawners[Random.Range(0, cloudSpawners.Count)]), Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
            cloud.transform.localScale *= 5;
        }
	}

    Vector3 RandomPointInsideOf(GameObject obj)
    {
        Vector3 point = Vector3.zero;
        point = obj.GetComponent<Collider>().bounds.center + obj.GetComponent<Collider>().ClosestPoint(new Vector3(Random.Range(-10000.0f, 10000.0f), Random.Range(-10000.0f, 10000.0f), Random.Range(-10000.0f, 10000.0f)));
        point /= 2;
        return point;
    }
}
