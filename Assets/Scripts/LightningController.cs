using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour {

    public GameObject circle;
    public GameObject spawner;
    public AudioSource lightningSource;
    public AudioClip lightningSound;

    public float lifetime = 1.5f;

    Timer timer;
    LightningSpawner lightning;

    public bool game =  true;
    // Use this for initialization
    void Start () {
        lightning = Instantiate(spawner, transform, false).GetComponent<LightningSpawner>();
        timer = new Timer(lifetime);
        lightning.transform.position = transform.position;
        lightning.transform.position += new Vector3(0.0f, 30.0f, 0.0f);
        lightning.midPoint = circle.transform.position;
        lightning.endPoint = circle.transform.position;
        lightningSource = GetComponent<AudioSource>();

        createLightningFromPoint(circle.transform.position, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
		if (timer.Trigger())
        {
            Destroy(gameObject);
            return;
        }
	}

    void createLightningFromPoint(Vector3 point, float maxDist, GameObject followStart = null)
    {
        lightningSource.PlayOneShot(lightningSound, 0.1f);
        if (game)
        {
            foreach (var boid in RobotZombieBehaviour.Instance.robotZombies)
            {
                if (!boid.GetComponent<BoidStats>().squished && boid.active)
                {
                    if ((boid.transform.position - point).sqrMagnitude < maxDist)
                    {
                        var light = Instantiate(spawner, transform, false).GetComponent<LightningSpawner>();
                        light.transform.position = point;
                        light.radius = 0.2f;
                        light.segments = (int)((boid.transform.position - point).sqrMagnitude);
                        //light.GetComponent<LineRenderer>().widthMultiplier = 0.3f;
                        //lightning.transform.position += new Vector3(0.0f, 30.0f, 0.0f);
                        light.Follow = boid;
                        if (followStart != null)
                        {
                            light.FollowStart = followStart;
                        }
                        boid.GetComponent<BoidStats>().squished = true;
                        StartCoroutine(boid.GetComponent<BoidStats>().fry(boid));
                        createLightningFromPoint(boid.transform.position, maxDist, boid);
                    }
                }
            }
        }
    }
}
