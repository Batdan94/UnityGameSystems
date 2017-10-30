using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {

    [SerializeField]
    float fireSpawnTime;
    [SerializeField]
    float fireLifeTime;
    [SerializeField]
    float minDistForFire;
    public GameObject circle;
    Timer spawningTimer;
    Timer lifeTimer;

    Vector3 lastFire;

    public AudioSource fireSource;
    public AudioClip fireSound;

    public GameObject firePrefab;

    // Use this for initialization
    void Start () {
        fireSource = GetComponent<AudioSource>();
        spawningTimer = new Timer(fireSpawnTime);
        lifeTimer = new Timer(fireLifeTime);
        Instantiate(firePrefab, circle.transform.position, Quaternion.identity, this.transform);
        lastFire = circle.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if ((spawningTimer.timeLeft > 0.0f))
        {
            fireSource.PlayOneShot(fireSound, 0.1f);
            if (Vector3.Distance(lastFire, circle.transform.position) > minDistForFire)
            {
                Instantiate(firePrefab, circle.transform.position, Quaternion.identity, this.transform);
                lastFire = circle.transform.position;
            }
        }
        if (lifeTimer.Trigger())
        {
            Destroy(gameObject);
        }
	}
}
