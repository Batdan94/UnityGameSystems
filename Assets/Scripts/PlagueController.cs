using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueController : MonoBehaviour {


    [SerializeField]
    float plagueSpawnTime;
    [SerializeField]
    float plagueLifeTime;
    [SerializeField]
    float minDistForInfect;
    public GameObject circle;
    Timer spawnTimer;
    Timer lifeTimer;


    public GameObject plaguePrefab;
    //Audio
    
	// Use this for initialization
	void Start ()
    {
        spawnTimer = new Timer(plagueSpawnTime);
        lifeTimer = new Timer(plagueLifeTime);
        Instantiate(plaguePrefab, circle.transform.position, Quaternion.identity, this.transform);

	}
	
	// Update is called once per frame
	void Update ()
    {
	    if ((spawnTimer.timeLeft > 0.0f))
        {
            Instantiate(plaguePrefab, circle.transform.position, Quaternion.identity, this.transform);

        }

        if(lifeTimer.Trigger())
        {
            Destroy(gameObject);
        }
	}
}
