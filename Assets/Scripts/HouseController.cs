using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

    public List<GameObject> houses;

    public List<GameObject> robotPrefabHouses;
    public List<GameObject> zombiePrefabHouses;

    // Use this for initialization
    void Start () {
        houses = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            houses.Add(transform.GetChild(i).gameObject);
        }
        
        foreach (var house in houses)
        {
            Instantiate(robotPrefabHouses[Random.Range(0, robotPrefabHouses.Count)], house.transform, false);
            house.GetComponent<MeshRenderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
