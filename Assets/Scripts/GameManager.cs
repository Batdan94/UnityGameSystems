using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;
	public UIManager UImngr;
    public int roundNumber = 0;

    public bool breeding;
    public bool foundMatches = false;
    public float loveDistance;

    public GameObject barriers;

    public List<ThreatValue> threats;
    //Timer roundTimer;
    [SerializeField]
    //float timePerRound;

	// Use this for initialization
	void Start () {
       //roundTimer = new Timer(timePerRound);
    }
	
	// Update is called once per frame
	void Update ()
    {
        threats.RemoveAll(threat => threat == null);

		if (!AnyZombosLeft())
        {
			//Remove all dead zombos
            foreach(var ro in BoidsManager.robotZombies)
            {
                if (ro.GetComponent<BoidStats>().squished)
                {
                    DestroyImmediate(ro);
                }
            }
            BoidsManager.robotZombies.RemoveAll(zombo => zombo == null);

			//Reset standard values
            BoidsManager.SetHasAttacked(false);
            foreach (var zombo in BoidsManager.robotZombies)
                if (zombo.active == false)
                {
                    zombo.SetActive(true);
                    zombo.transform.position = BoidsManager.GetComponent<RobotZombieBehaviour>().getRandomSpawn();
                    zombo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    zombo.GetComponent<BoidStats>().hasBred = false; 
                }

            //Breeding
            breeding = true;

            //calculate distance from goal if there is one

        }
        if (breeding)
        {
            barriers.SetActive(true);
            if (!foundMatches)
            {

                foreach (var zombo in BoidsManager.robotZombies)
                {
                    BoidStats ZomboBS = zombo.GetComponent<BoidStats>();
                    if (ZomboBS.lovedOne == null)
                    {
                        GameObject closest = ZomboBS.findClosestZombot();
                        ZomboBS.lovedOne = closest;
                        closest.GetComponent<BoidStats>().lovedOne = zombo;
                    }
                }

                //var combo = (from left in BoidsManager.robotZombies
                //             from right in BoidsManager.robotZombies
                //             where right != left
                //             where (right.GetComponent<BoidStats>().lovedOne == null && left.GetComponent<BoidStats>().lovedOne == null)
                //             where (right.GetComponent<BoidStats>().lovedOne = left)
                //             where (left.GetComponent<BoidStats>().lovedOne = right)
                //             select new { left, right }).ToArray();

                //TODO Display breeding text and stop attacks
                //int initialZombos = BoidsManager.robotZombies.Count;
                //for (int i = 0; i < initialZombos; i++)
                //{

                //    GameObject closestZombo = null;
                //    //Find closest ZombieRobot to this zombo
                //    for (int j = i + 1; j < initialZombos; j++)
                //    {
                //        if (closestZombo == null)
                //            closestZombo = BoidsManager.robotZombies[j];
                //        if (BoidsManager.robotZombies[i] != BoidsManager.robotZombies[j] && !BoidsManager.robotZombies[j].GetComponent<BoidStats>().hasBred)
                //            if (Vector3.Distance(BoidsManager.robotZombies[i].transform.position, BoidsManager.robotZombies[j].transform.position) <
                //                Vector3.Distance(BoidsManager.robotZombies[i].transform.position, closestZombo.transform.position))
                //                closestZombo = BoidsManager.robotZombies[j];
                //    }

                //    BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne = closestZombo;
                //}
                foundMatches = true;
            }
            else
            {
                int initialZombos = BoidsManager.robotZombies.Count;
                for (int i = 0; i < initialZombos; i++)
                {
                    if (!BoidsManager.robotZombies[i].GetComponent<BoidStats>().hasBred && BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne != null)
                    {
                        if (Vector3.Distance(BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne.transform.position, BoidsManager.robotZombies[i].transform.position) > loveDistance)
                        {
                            BoidsManager.robotZombies[i].GetComponent<NavMeshAgent>().enabled = true;
                            BoidsManager.robotZombies[i].GetComponent<NavMeshAgent>().destination = BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne.transform.position;
                        }
                        else
                        {
                            BoidsManager.robotZombies[i].GetComponent<NavMeshAgent>().enabled = false;
                            BoidsManager.robotZombies.Add(BoidStats.breed(BoidsManager.robotZombies[i].GetComponent<BoidStats>(), BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne.GetComponent<BoidStats>(), BoidsManager.prefab));
                            BoidsManager.robotZombies[i].GetComponent<BoidStats>().lovedOne.GetComponent<BoidStats>().hasBred = true;
                            BoidsManager.robotZombies[i].GetComponent<BoidStats>().hasBred = true;
                        }
                    }
                }
            }

            // go back to play mode
            foreach(var zombot in BoidsManager.robotZombies)
            {
                if (!zombot.GetComponent<BoidStats>().hasBred && zombot.GetComponent<BoidStats>().lovedOne != null)
                {
                    return;
                }
                else
                {
                    zombot.GetComponent<NavMeshAgent>().enabled = false;
                }
            }
            roundNumber++;
            breeding = false;
            foundMatches = false;
            barriers.SetActive(false);

            //TODO remove breeding text and start attacks again
        }
    }

    bool AnyZombosLeft()
    {
        foreach (var zombo in BoidsManager.robotZombies)
            if (zombo.active && !zombo.GetComponent<BoidStats>().squished)
                return true;

        return false; 
    }
}
