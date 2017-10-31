using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueController : MonoBehaviour {


    [SerializeField]
    public float plagueLifeTime;
    [SerializeField]
    float minDistForInfect;
    public GameObject circle;
    Timer lifeTimer;


    public GameObject plaguePrefab;
    //Audio
    
	// Use this for initialization
	void Start ()
    {
        lifeTimer = new Timer(plagueLifeTime);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(lifeTimer.Trigger())
        {
            Destroy(gameObject);
        }

	}
}
