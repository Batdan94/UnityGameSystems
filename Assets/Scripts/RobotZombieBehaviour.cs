using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotZombieBehaviour : Singleton<RobotZombieBehaviour>
{
    public bool DebugDraw;

    public List<GameObject> robotZombies;
    public GameObject prefab;
    [Range(0, 100)]
    public int numZombos;
    [Range(0, 50)]
    public float spawnRange;
    [Range(0.0f, 10.0f)]
    public float speed;
    [Range(1.0f, 10.0f)]
    public float spawnHeight;
    [Range(0.0f, 10.0f)]
    public float edgeRepelForce;
    [Range(0.0f, 100.0f)]
    public float fleeForce;
    [Range(0.1f, 1.0f)]
    public float edgeRepelIntensity;
    [Range(1.0f, 5.0f)]
    public float separationModifier;
    [Range(1.0f, 5.0f)]
    public float alignmentModifier;
    [Range(1.0f, 5.0f)]
    public float cohesionModifier;
	[Range(1.0f, 40.0f)]
	public float threatModifier;
    [Range(0.0f, 10.0f)]
    public float separationDistance;
    [Range(0.0f, 10.0f)]
    public float alignmentDistance;
    [Range(0.0f, 10.0f)]
    public float cohesionDistance;

    public float minSpeed;
    public float maxSpeed;
    private bool hasAttacked = false;
    private bool hasBred = false; 
    public GameObject plane; 
    public void SetHasAttacked(bool set) { hasAttacked = set; }
    public void SetHasbred(bool set) { hasBred = set; }
    public bool HasBred() { return hasBred; }
    public Vector3 getRandomSpawn() { return new Vector3(Random.Range(-spawnRange, spawnRange), spawnHeight, Random.Range(-spawnRange, spawnRange)); }
    // Use this for initialization
    void Start ()
    {
        robotZombies = new List<GameObject>();
        if (spawnRange > plane.transform.localScale.x * 5)
            spawnRange = plane.transform.localScale.x * 5; 
        SpawnZombos(numZombos); 
	}

    void SpawnZombos(int numzombos)
    {
        for (int i = 0; i < numzombos; ++i)
        {
            //robotZombies[i] = new GameObject("Robot Zombie");
            Debug.Log("Spawned Zombo");
            Vector3 location = getRandomSpawn(); 
            robotZombies.Add(Instantiate(prefab, location, Quaternion.identity));
            robotZombies[i].transform.parent = transform;
            //robotZombies[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ; 
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 v1 = Vector3.zero;
        Vector3 v2 = Vector3.zero;
        Vector3 v3 = Vector3.zero;
		Vector3 v4 = Vector3.zero;
        Vector3 v5 = Vector3.zero; 
        //Get typelist
        for (int i = 0; i < robotZombies.Count; ++i)
        {
            if (robotZombies[i].GetComponent<BoidStats>().squished)
            {
                continue;
            }
            Rigidbody rb = robotZombies[i].GetComponent<Rigidbody>();
            v1 = Separation(i);
            v2 = Alignment(i);
            v3 = Cohesion(i);
			v4 = Threat(i);
            if (hasAttacked == true)
            v5 = Flee(i); 
            Vector3 velocity = v1 + v2 + v3 + v4 + v5;
			velocity.y = 0.0f;
			if (!(Vector3.Dot (Vector3.up, robotZombies[i].transform.up) <= 0.2f))
			{
				rb.AddForce (velocity * speed * Time.deltaTime);
	            rb.AddForce(EdgeAvoidance(rb.velocity, i));
				//rb.velocity = new Vector3(rb.velocity.x, Mathf.Min(0.0f, rb.velocity.y), rb.velocity.z);
	            if (rb.velocity.magnitude > maxSpeed)
	            {
					var vel = rb.velocity.normalized * maxSpeed;
					vel.y = -0.1f;
					rb.velocity = vel;
	            }
	            else if (rb.velocity.magnitude < minSpeed)
	            {
					var vel = rb.velocity.normalized * minSpeed;
					vel.y = 0.0f;
					rb.velocity = vel;
	            }
			}

			Vector3 dir = rb.velocity;
			float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
			rb.transform.localRotation = Quaternion.Lerp(rb.transform.localRotation, Quaternion.AngleAxis(angle, Vector3.up), 0.1f);
            //robotZombies[i].GetComponent<Rigidbody>().velocity.Normalize(); 

        }
    }

    Vector3 Separation(int i)
    {
        Vector3 separationForce = Vector3.zero; 
        
        foreach (GameObject ro in robotZombies)
        {
            if (ro.GetComponent<BoidStats>().squished)
            {
                continue;
            }
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < separationDistance && dist != 0)
            {
                separationForce += ((robotZombies[i].transform.position - ro.transform.position) / dist) * separationModifier;
                if (DebugDraw)
                    Debug.DrawLine(robotZombies[i].transform.position, robotZombies[i].transform.position + ((robotZombies[i].transform.position - ro.transform.position) / dist) * separationModifier, Color.red);
            }
            
        }
        return separationForce; 
    }
    Vector3 Alignment(int i)
    {
        Vector3 alignmentForce = Vector3.zero; 
        foreach (GameObject ro in robotZombies)
        {
            if (ro.GetComponent<BoidStats>().squished)
            {
                continue;
            }
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < alignmentDistance && dist != 0)
            {
                alignmentForce += (ro.GetComponent<Rigidbody>().velocity / (1+dist)) * alignmentModifier;
                if (DebugDraw)
                    Debug.DrawLine(robotZombies[i].transform.position, robotZombies[i].transform.position + (ro.GetComponent<Rigidbody>().velocity / dist) * alignmentModifier, Color.blue);

            }
        }
    
                return alignmentForce.normalized;
    }
    Vector3 Cohesion(int i)
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (GameObject ro in robotZombies)
        {
            if (ro.GetComponent<BoidStats>().squished)
            {
                continue;
            }
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < cohesionDistance && dist != 0)
            {
                centerOfMass += ((ro.transform.position - robotZombies[i].transform.position) / dist) * cohesionModifier;
                if (DebugDraw)
                    Debug.DrawLine(robotZombies[i].transform.position, robotZombies[i].transform.position + (((ro.transform.position - robotZombies[i].transform.position) / dist) * cohesionModifier), Color.green);
            }
            
        }
       return centerOfMass;
    }

	Vector3 Threat(int i)
	{
		var vec = new Vector3 ();
		foreach (var tr in GameObject.FindObjectsOfType<ThreatValue>()) {

			if (Vector3.Distance (tr.transform.position, robotZombies [i].transform.position) < tr.threat * 5) {
				vec += (robotZombies [i].transform.position - tr.transform.position).normalized * tr.threat * threatModifier;
			}
		}
		return vec;
	}

    Vector3 Flee(int i)
    {
        Vector3 fleeDirection = Vector3.zero;
        float distX = Mathf.Abs(robotZombies[i].transform.position.x - (plane.transform.localScale.x * 5));
        float distZ = Mathf.Abs(robotZombies[i].transform.position.z - (plane.transform.localScale.z * 5));
        if (fleeForce < 100)
        fleeForce = fleeForce + 0.001f; 
        if (distX < 1 || distZ < 1)
            robotZombies[i].SetActive(false);
        if (distX > distZ)
            fleeDirection = new Vector3(0, 0, fleeForce);
        else
            fleeDirection = new Vector3(fleeForce, 0, 0);
        return fleeDirection; 
    }

    Vector3 EdgeAvoidance(Vector3 v, int i)
    {
        v = new Vector3();
        float dist = Mathf.Abs(robotZombies[i].transform.position.x - (plane.transform.localScale.x * 5));
        if (dist < 5)
        {
            v.x += -(edgeRepelForce / dist) * edgeRepelIntensity;
        }
        float dist1 = Mathf.Abs(robotZombies[i].transform.position.x - (-plane.transform.localScale.x * 5));
        if (dist1 < 5)
        {
            v.x += (edgeRepelForce / dist1) * edgeRepelIntensity;
        }
        float dist2 = Mathf.Abs(robotZombies[i].transform.position.z - (plane.transform.localScale.z * 5));
        if (dist2 < 5)
        {
            v.z += -(edgeRepelForce / dist2) * edgeRepelIntensity;
        }
        float dist3 = Mathf.Abs(robotZombies[i].transform.position.z - (-plane.transform.localScale.z * 5));
        if (dist3 < 5)
        {
            v.z += (edgeRepelForce / dist3) * edgeRepelIntensity;
        }
        if (DebugDraw)
            Debug.DrawLine(robotZombies[i].transform.position, robotZombies[i].transform.position + v, Color.yellow);
        return v;
    }
}
