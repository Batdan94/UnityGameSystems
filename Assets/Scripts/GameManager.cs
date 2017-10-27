using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;

    public int roundNumber = 0;
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
		if (!AnyZombosLeft())
        {
            //END OF ROUND CODE
            //spawn zombos until we reach the right number
            foreach(var ro in BoidsManager.robotZombies)
            {
                if (ro.GetComponent<BoidStats>().squished)
                {
                    DestroyImmediate(ro);
                }
            }
            BoidsManager.robotZombies.RemoveAll(zombo => zombo == null);
            BoidsManager.SetHasAttacked(false);
            foreach (var zombo in BoidsManager.robotZombies)
                if (zombo.active == false)
                {
                    zombo.SetActive(true);
                    zombo.transform.position = BoidsManager.GetComponent<RobotZombieBehaviour>().getRandomSpawn();
                    zombo.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    zombo.GetComponent<BoidStats>().hasBred = false; 
                }

            int initialZombos = BoidsManager.robotZombies.Count;
            foreach (var zombo in BoidsManager.robotZombies)
            {
                GameObject closestZombo = null;
                if (zombo.GetComponent<BoidStats>().hasBred)
                    return; 
                //Find closest ZombieRobot to this zombo
                foreach (var zomb in BoidsManager.robotZombies)
                {
                    if (closestZombo == null)
                        closestZombo = zomb;
                    if (zombo != zomb && !zomb.GetComponent<BoidStats>().hasBred)
                        if (Vector3.Distance(zombo.transform.position, zomb.transform.position) < Vector3.Distance(zombo.transform.position, closestZombo.transform.position))
                            closestZombo = zomb; 
                }
                BoidsManager.robotZombies.Add(BoidStats.breed(zombo.GetComponent<BoidStats>(), closestZombo.GetComponent<BoidStats>(), BoidsManager.prefab));
                zombo.GetComponent<BoidStats>().hasBred = true;
                closestZombo.GetComponent<BoidStats>().hasBred = true; 
            }
            //calculate distance from goal if there is one

            // go back to play mode
            roundNumber++;

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
