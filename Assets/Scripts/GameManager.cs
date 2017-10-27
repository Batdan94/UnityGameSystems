using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;
	public UIManager UImngr;
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
            int initialZombos = BoidsManager.robotZombies.Count;
			for (int i = 0; i < initialZombos ; i++)
			{
				GameObject closestZombo = null;
				if (BoidsManager.robotZombies[i].GetComponent<BoidStats>().hasBred)
                    return; 
                //Find closest ZombieRobot to this zombo
				for (int j = i+1; j < initialZombos ; j++)
				{
                    if (closestZombo == null)
						closestZombo = BoidsManager.robotZombies[j];
					if (BoidsManager.robotZombies[i] != BoidsManager.robotZombies[j] && !BoidsManager.robotZombies[j].GetComponent<BoidStats>().hasBred)
					if (Vector3.Distance(BoidsManager.robotZombies[i].transform.position, BoidsManager.robotZombies[j].transform.position) < 
						Vector3.Distance(BoidsManager.robotZombies[i].transform.position, closestZombo.transform.position))
						closestZombo = BoidsManager.robotZombies[j]; 
                }
				BoidsManager.robotZombies.Add(BoidStats.breed(BoidsManager.robotZombies[i].GetComponent<BoidStats>(), closestZombo.GetComponent<BoidStats>(), BoidsManager.prefab));
				BoidsManager.robotZombies[i].GetComponent<BoidStats>().hasBred = true;
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
