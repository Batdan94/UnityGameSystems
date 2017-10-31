using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;
	public UIManager UImngr;
    public int roundNumber;
	public int numSquished;
    public bool breeding;
    public bool foundMatches = false;
    public float loveDistance;
    public int minimumZombies; 
    public GameObject barriers;

    float mxBreedTime = 4.0f;
    Timer breedTimer;

    public List<ThreatValue> threats;
    //Timer roundTimer;
    [SerializeField]
    //float timePerRound;

	// Use this for initialization
	void Start () {
       //roundTimer = new Timer(timePerRound);
		roundNumber = 0;
		numSquished = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        threats.RemoveAll(threat => threat == null);

        if (!AnyZombosLeft())
        {
            //Remove all dead zombos
            foreach (var ro in BoidsManager.robotZombies)
            {
                if (ro.GetComponent<BoidStats>().squished)
                {
                    DestroyImmediate(ro);
                    numSquished++;
                }
            }
            BoidsManager.robotZombies.RemoveAll(zombo => zombo == null);

            //Reset standard values
            BoidsManager.SetHasAttacked(false);
            int checkEnoughZombos = 0;
            foreach (var zombo in BoidsManager.robotZombies)
                if (zombo.active == false)
                {
                    zombo.SetActive(true);
                    zombo.transform.position = BoidsManager.GetComponent<RobotZombieBehaviour>().getRandomSpawn();
                    zombo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    zombo.GetComponent<BoidStats>().hasBred = false;
                    checkEnoughZombos++;
                }
            if (checkEnoughZombos < minimumZombies)
            { 
                Application.LoadLevel(3);
            }
            BoidsManager.GetComponent<RobotZombieBehaviour>().fleeForce = 0; 
            //Breeding
            breeding = true;
            foreach(var obj in GameObject.FindGameObjectsWithTag("Attack"))
            {
                Destroy(obj);
            }

            foreach (var obj in FindObjectsOfType<ThreatValue>())
            {
                Destroy(obj.gameObject);
            }
            breedTimer = new Timer(mxBreedTime);
            //calculate distance from goal if there is one

        }
        if (breeding)
        {
            if (breedTimer.Trigger())
            {
                foreach (var zombot in BoidsManager.robotZombies)
                {
                    zombot.GetComponent<NavMeshAgent>().enabled = false;
                }
                roundNumber++;
                breeding = false;
                foundMatches = false;
                barriers.SetActive(false);
                FindObjectOfType<TemporaryCircleDeath>().enabled = true;
                return;
            }
            barriers.SetActive(true);
            if (!foundMatches)
            {
                foreach (var zombo in BoidsManager.robotZombies)
                {
                    BoidStats ZomboBS = zombo.GetComponent<BoidStats>();
                    if (ZomboBS.lovedOne == null)
                    {
                        GameObject closest = ZomboBS.findClosestZombot();
                        if (closest != null)
                        {
                            ZomboBS.lovedOne = closest;
                            closest.GetComponent<BoidStats>().lovedOne = zombo;
                        }
                    }
                }

                //TODO Display breeding text and stop attacks
                FindObjectOfType<TemporaryCircleDeath>().enabled = false;

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
            FindObjectOfType<TemporaryCircleDeath>().enabled = true;


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
