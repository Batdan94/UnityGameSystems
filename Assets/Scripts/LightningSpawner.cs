using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {

    [SerializeField]
    GameObject LightningPrefab;

    [SerializeField]
    Material[] LightningMats;

    public GameObject Follow;
    public GameObject FollowStart;

    public Vector3 endPoint;
    public Vector3 midPoint;
    public float radius;
    public int segments;
    public Color color;
    public float bezierTimer;

    // Use this for initialization
    void Awake () {
        radius = 0.4f;
        segments = 20;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(LightningPrefab);
            if (FollowStart!= null)
            {
                go.transform.position = FollowStart.transform.position;
            }
            if (Follow != null)
            {
                go.GetComponent<Lightning>().endPoint = Follow.transform.position;
                go.GetComponent<Lightning>().midPoint = Follow.transform.position + Vector3.up;
                
            }
            else
            {
                go.GetComponent<Lightning>().endPoint = endPoint;
                go.GetComponent<Lightning>().midPoint = midPoint;
            }
            go.GetComponent<Lightning>().radius = radius;
            go.GetComponent<Lightning>().segments = segments;
            go.GetComponent<Lightning>().color = color;
            go.GetComponent<Lightning>().bezierTimer = bezierTimer;
            go.GetComponent<Lightning>().lightningMat = LightningMats[0];
            go.transform.position = transform.position;
            go.transform.parent = this.transform;
            go.GetComponent<Lightning>().ready();
        }
    }
}
