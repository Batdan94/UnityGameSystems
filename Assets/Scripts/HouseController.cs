using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

	public List<GameObject> houses;
    public List<GameObject> housesLayer1;
	public List<GameObject> robotPrefabHousesLayer1;
    public List<GameObject> robotPrefabHouses;

    public float zombificationLevel = 5.0f;

    public List<MeshRenderer> zombieBit;
	public List<float> zombieMod;

    // Use this for initialization
    void Start () {
        houses = new List<GameObject>();
		zombieBit = new List<MeshRenderer> ();
		zombieMod = new List<float> ();
        for (int i = 0; i < transform.childCount; i++)
        {
			if (!housesLayer1.Contains(transform.GetChild(i).gameObject))
            	houses.Add(transform.GetChild(i).gameObject);
        }
        
        foreach (var house in houses)
        {
            var inst = Instantiate(robotPrefabHouses[Random.Range(0, robotPrefabHouses.Count)], house.transform, false);
			zombieBit.Add(inst.transform.Find ("ZombieWall").GetComponentInChildren<MeshRenderer> ());
			zombieMod.Add (Utils.NextGaussian ());
			for (int i = 0; i < zombieBit [zombieBit.Count - 1].materials.Length; i++) {
				zombieBit [zombieBit.Count - 1].materials [i] = Instantiate (zombieBit [zombieBit.Count - 1].materials [i]);
				float rb = Random.Range (0, 141);
				zombieBit [zombieBit.Count - 1].materials [i].SetColor("_color", new Color (rb, Random.Range (140, 255), rb));
			
			}
            house.GetComponent<MeshRenderer>().enabled = false;
        }
		foreach (var house in housesLayer1)
		{
			var inst = Instantiate(robotPrefabHousesLayer1[Random.Range(0, robotPrefabHousesLayer1.Count)], house.transform, false);
			zombieBit.Add(inst.transform.Find ("ZombieWall").GetComponentInChildren<MeshRenderer> ());
			zombieMod.Add (Utils.NextGaussian ());
			for (int i = 0; i < zombieBit [zombieBit.Count - 1].materials.Length; i++) {
				zombieBit [zombieBit.Count - 1].materials [i] = Instantiate (zombieBit [zombieBit.Count - 1].materials [i]);
				float rb = Random.Range (0, 141);
				zombieBit [zombieBit.Count - 1].materials [i].SetColor("_color", new Color (rb, Random.Range (140, 255), rb));

			}
			house.GetComponent<MeshRenderer>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
        float tempPopulation = 0;
        int count = 0;
        foreach (var rz in RobotZombieBehaviour.Instance.robotZombies)
        {
            if (rz.GetComponent<BoidStats>().squished == false)
            {
                tempPopulation += rz.GetComponent<BoidStats>().heatlh;
                count++;
            }
        }

        var zombieLevel = (tempPopulation / count);

		for ( int j = 0; j < zombieBit.Count; j++)
        {
			for (int i = 0; i < zombieBit[j].materials.Length; i++){
				zombieBit[j].materials[i].SetFloat ("_Displacement", ( zombieLevel / 2) + zombieMod[j]);
				/*if (Random.value > 0.999) {
					zombieBit [j].materials [i].color = new Color (zombieBit [j].materials [i].color.r, 
						zombieBit [j].materials [i].color.g + Utils.NextGaussian (),
						zombieBit [j].materials [i].color.b);
				}*/
				zombieBit [j].enabled = zombieLevel > 2.0f;
			}

        }
    }
}
