using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotZombieBehaviour : MonoBehaviour
{
    public GameObject[] robotZombies;
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
    [Range(0.1f, 1.0f)]
    public float edgeRepelIntensity;
    [Range(1.0f, 5.0f)]
    public float seperationModifier;
    [Range(1.0f, 5.0f)]
    public float alignmentModifier;
    [Range(1.0f, 5.0f)]
    public float cohesionModifier;
    [Range(0.0f, 10.0f)]
    public float seperationDistance;
    [Range(0.0f, 10.0f)]
    public float alignmentDistance;
    [Range(0.0f, 10.0f)]
    public float cohesionDistance;
    
    public GameObject plane; 

	// Use this for initialization
	void Start ()
    {
        robotZombies = new GameObject[numZombos];
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
            Vector3 location = new Vector3(Random.Range(-spawnRange, spawnRange), spawnHeight, Random.Range(-spawnRange, spawnRange));
            robotZombies[i] = Instantiate(prefab, location, Quaternion.identity) as GameObject;
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
        //Get typelist
        for (int i = 0; i < robotZombies.Length; ++i)
        {
            v1 = Separation(i) * seperationModifier;
            v2 = Alignment(i) * alignmentModifier;
            v3 = Cohesion(i) * cohesionModifier;
            Vector3 velocity = v1 + v2 + v3;
            robotZombies[i].GetComponent<Rigidbody>().AddForce(velocity * speed * Time.deltaTime);
            robotZombies[i].GetComponent<Rigidbody>().velocity = EdgeAvoidance(robotZombies[i].GetComponent<Rigidbody>().velocity, i);
            //robotZombies[i].GetComponent<Rigidbody>().velocity.Normalize(); 

        }
    }

    Vector3 Separation(int i)
    {
        Vector3 separationForce = Vector3.zero; 
        
        foreach (GameObject ro in robotZombies)
        {
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < seperationDistance && dist != 0)
                separationForce += (ro.transform.position + robotZombies[i].transform.position) / dist;
            
        }
        return separationForce; 
    }
    Vector3 Alignment(int i)
    {
        Vector3 alignmentForce = Vector3.zero; 
        foreach (GameObject ro in robotZombies)
        {
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < alignmentDistance && dist != 0)
                    alignmentForce += ro.GetComponent<Rigidbody>().velocity / dist; 
        }
    
                return alignmentForce.normalized;
    }
    Vector3 Cohesion(int i)
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (GameObject ro in robotZombies)
        {
            float dist = Vector3.Distance(robotZombies[i].transform.position, ro.transform.position);
            if (robotZombies[i] != ro && dist < cohesionDistance && dist != 0)
                  centerOfMass -= ro.transform.position / dist;
            
        }
       return centerOfMass;
    }

    Vector3 EdgeAvoidance(Vector3 v, int i)
    {
        float dist = Mathf.Abs(robotZombies[i].transform.position.x - (plane.transform.localScale.x * 5));
        if (dist < 5)
        {
            v.x = -edgeRepelForce / (dist * edgeRepelIntensity);
        }
        float dist1 = Mathf.Abs(robotZombies[i].transform.position.x - (-plane.transform.localScale.x * 5));
        if (dist1 < 5)
        {
            v.x = edgeRepelForce / (dist1 * edgeRepelIntensity);
        }
        float dist2 = Mathf.Abs(robotZombies[i].transform.position.z - (plane.transform.localScale.z * 5));
        if (dist2 > -5)
        {
            v.z = -edgeRepelForce / (dist2 * edgeRepelIntensity);
        }
        float dist3 = Mathf.Abs(robotZombies[i].transform.position.z - (-plane.transform.localScale.z * 5));
        if (dist3 < 5)
        {
            v.z = edgeRepelForce / (dist3 * edgeRepelIntensity);
        }
        return v;
    }
}
