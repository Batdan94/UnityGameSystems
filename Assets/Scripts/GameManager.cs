using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;

    public int roundNumber = 0;
    Timer roundTimer;
    [SerializeField]
    float timePerRound;

	// Use this for initialization
	void Start () {
       roundTimer = new Timer(timePerRound);
    }
	
	// Update is called once per frame
	void Update () {
		if (roundTimer.Trigger())
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
            int initialZombos = BoidsManager.robotZombies.Count;
            for (int i = 0; i < BoidsManager.numZombos - initialZombos; i++)
            {
                BoidsManager.robotZombies.Add(BoidStats.breed(BoidsManager.robotZombies[Random.Range(0, initialZombos)].GetComponent<BoidStats>(), 
                                                                BoidsManager.robotZombies[Random.Range(0, initialZombos)].GetComponent<BoidStats>(), 
                                                                BoidsManager.prefab));
            }
            //calculate distance from goal if there is one

            // go back to play mode
            roundNumber++;

        }
	}
}
