using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    RobotZombieBehaviour BoidsManager;

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
            roundNumber++;
        }
	}
}
