using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotZombieBehaviour : MonoBehaviour
{
    public GameObject[] robotZombies;
    public GameObject prefab;
    public int numZombos;
    public float range;
    public float speed;
    public float spawnHeight; 
    public float seperationDistance;
    public float alignmentDistance;
    public float cohesionDistance;
    public GameObject plane; 

	// Use this for initialization
	void Start ()
    {
        robotZombies = new GameObject[numZombos]; 
        SpawnZombos(numZombos); 
	}

    void SpawnZombos(int numzombos)
    {
        for (int i = 0; i < numzombos; ++i)
        {
            //robotZombies[i] = new GameObject("Robot Zombie");
            Debug.Log("Spawned Zombo"); 
            Vector3 location = new Vector3(Random.Range(-range, range), spawnHeight, Random.Range(-range, range));
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
            v1 = Separation(i);
            v2 = Alignment(i);
            v3 = Cohesion(i);
            Vector3 velocity = v1 + v2 + v3;
            Vector3 acceleration = EdgeAvoidance(velocity, i) * Time.deltaTime;
            robotZombies[i].GetComponent<Rigidbody>().AddForce(acceleration); 
            robotZombies[i].GetComponent<Rigidbody>().velocity.Normalize(); 
        }
    }

    Vector3 Separation(int i)
    {
        Vector3 separationForce = Vector3.zero; 
        
        foreach (GameObject ro in robotZombies)
        {
            if (robotZombies[i] != ro)
            if (Vector3.Distance(robotZombies[i].transform.position, ro.transform.position) < seperationDistance)
            {
                //Debug.Log("Distance = " + Vector3.Distance(robotZombies[i].transform.position, ro.transform.position).ToString());
                separationForce += Vector3.MoveTowards(robotZombies[i].transform.position, ro.transform.position, speed * Time.deltaTime);
            }
        }
        return separationForce; 
    }
    Vector3 Alignment(int i)
    {
        Vector3 alignmentForce = Vector3.zero; 
        foreach (GameObject ro in robotZombies)
        {
            if (robotZombies[i] != ro)
                if (Vector3.Distance(robotZombies[i].transform.position, ro.transform.position) < alignmentDistance)
                {
                    alignmentForce += ro.GetComponent<Rigidbody>().velocity; 
                }
        }
    
                return alignmentForce;
    }
    Vector3 Cohesion(int i)
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (GameObject ro in robotZombies)
        {
            if (robotZombies[i] != ro)
            {
                if (Vector3.Distance(robotZombies[i].transform.position, ro.transform.position) < cohesionDistance)
                {
                    centerOfMass += ro.transform.position;
                }
            }
        }
       return centerOfMass;
    }

    Vector3 EdgeAvoidance(Vector3 v, int i)
    {
        if (robotZombies[i].transform.position.x < (plane.transform.localScale.x * 5) - 1)
        {
            v.x = -3;
        }
        if (robotZombies[i].transform.position.x > -(plane.transform.localScale.x * 5) + 1)
        {
            v.x = 3;
        }
        if (robotZombies[i].transform.position.z < (plane.transform.localScale.z * 5) - 1)
        {
            v.z = -3;
        }
        if (robotZombies[i].transform.position.z > (plane.transform.localScale.z * 5) + 1)
        {
            v.z = 3;
        }
        return v;
    }
}
