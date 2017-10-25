using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public RobotZombieBehaviour BoidsManager;

    int roundNumber = 0;
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

            //calculate distance from goal if there is one

            // go back to play mode
            roundNumber++;
        }
	}
}
