using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //gameobjects references
    public GameObject fist;
    public GameObject lightningFist;
    public GameObject fireFist;
    public GameObject plagueFist;

    public Text roundText;
    public Text objectivesText;

    //slider objects
    public Slider sizeSliderGO;
    public Slider wealthSliderGO;
    public Slider populationSliderGO;

	//temp floats
	float averageWealth;
	float averageSize;
	float averagePopulation;
	int numberOfSpawned;

	public Circle circle;

    //manager references
    private RobotZombieBehaviour zombieMngr;
	private TemporaryCircleDeath getNum;

    //objectives list
    string[] objectivesList;
    enum SocialDifferences
    {
        Small, 
        Big, 
        Zombie, 
        Robot, 
        Rich, 
        Poor
    }
    SocialDifferences socialDifferences; 
    string[] subject = { "These ", "The  ", "Those ", "Some " } ;
    string[] predicate = { " should be culled", " should be destroyed", " Need to be crushed", " Need to be eradicated", " Should be more dead" };
    string[] clause = { ", I'd like that", ", That would please me", ", Then everything would be better", ", Then they'd look more like me!"};


    // Use this for initialization
    void Start()
    {
		wealthSliderGO.minValue = 0;
		sizeSliderGO.minValue = 0;
		populationSliderGO.minValue = 0;

		wealthSliderGO.maxValue = 10;
		sizeSliderGO.maxValue = 10;
		populationSliderGO.maxValue = 10;

		zombieMngr = RobotZombieBehaviour.Instance;
		numberOfSpawned = zombieMngr.numZombos;
        objectivesList = new string[12];

        objectivesList[0] = "Have a 100% Zombie population";
        objectivesList[1] = "Have a 100% Robot population";

        objectivesList[2] = "Have a 100% large population";
        objectivesList[3] = "Have a 100% small population";

        objectivesList[4] = "Have a wealthy zombie population";
        objectivesList[5] = "Have a wealthy robot population";

        objectivesList[6] = "Have a poor zombie population";
        objectivesList[7] = "Have a poor robot population";

        objectivesList[8] = "Have a large zombie population";
        objectivesList[9] = "Have a large robot population";

        objectivesList[10] = "Have a small zombie population";
        objectivesList[11] = "Have a small robot population";
		objectives ();
    }

    string GenerateObjective(string socialDifference, int difficulty)
    {
        Random rnd = new Random(); 
        string output = "";
        output += subject[Random.Range(0, 3)] + socialDifference + " people";
        output += predicate[Random.Range(0, 4)];
        output += clause[Random.Range(0, 3)];
        return output; 
    }

    // Update is called once per frame
    void Update()
    {
		//numberOfSpawned = (zombieMngr.numZombos - getNum.numSquished);//zombieMngr.numZombos;
		updateAlive ();
        updateSliders();
        generationCounter();
        checkObjective(); 
    }

    void checkObjective()
    {
        switch (socialDifferences)
        {
            case SocialDifferences.Big:
                if (averageSize < 2)
                    NewObjective();
                break;
            case SocialDifferences.Small:
                if (averageSize > 8)
                    NewObjective();
                break;
            case SocialDifferences.Rich:
                if (averageWealth < 2)
                    NewObjective();
                break;
            case SocialDifferences.Poor:
                if (averageWealth > 8)
                    NewObjective();
                break;
            case SocialDifferences.Robot:
                if (averageSize < 2)
                    NewObjective();
                break;
            case SocialDifferences.Zombie:
                if (averageSize > 8)
                    NewObjective();
                break;
        }
    }
    void NewObjective()
    {
        
        int x = (int)socialDifferences; 
            
        while (x == (int)socialDifferences)
            x = Random.Range(0, 5);

        socialDifferences = (SocialDifferences)x;
        objectives();
        Debug.Log("New Objective" + socialDifferences.ToString());
    }
	void updateAlive()
	{
		numberOfSpawned = 0;
		foreach (var na in zombieMngr.robotZombies) {
			if (na.GetComponent<BoidStats> ().squished == false) {
				numberOfSpawned++;
			}
		}
	}

    void updateSliders()
    {
        wealthSlider();
        sizeSlider();
        populationSlider();
    }

     void wealthSlider()
    {
        float tempWealth = 0;
        int count = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempWealth += rz.GetComponent<BoidStats>().wealth;
                count++;

            }
        }

		averageWealth = (tempWealth / count);

        wealthSliderGO.value = (averageWealth);
    }

    void populationSlider()
    {
        float tempPopulation = 0;
        int count = 0;
        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempPopulation += rz.GetComponent<BoidStats>().heatlh;
                count++;
            }
        }

		averagePopulation = (tempPopulation / count);

        populationSliderGO.value = (averagePopulation); 
    }

    void sizeSlider()
    {
        float tempSize = 0;
        int count = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempSize += rz.GetComponent<BoidStats>().size;
                count++;

			}
        }
        averageSize = (tempSize / count);

		sizeSliderGO.value = (averageSize);
    }

    void objectives()
    {
        objectivesText.text = GenerateObjective(socialDifferences.ToString(), 1); 
            //= "Objective: \n\n" +
			//"" + objectivesList[Random.Range(0,11)]; 
    }

    void generationCounter()
    {
        roundText.GetComponent<Text>().text = "Gen Num: " + GameManager.Instance.roundNumber;
    }

   public void punchPower()
    {
		circle.xradius = 3.0f;
		circle.zradius = 3.0f;
		circle.CreatePoints();
		FindObjectOfType<TemporaryCircleDeath>().fist = fist;
    }

	public void LightningPower()
    {
		circle.xradius = 1.0f;
		circle.zradius = 1.0f;
		circle.CreatePoints();
		FindObjectOfType<TemporaryCircleDeath>().fist = lightningFist;
    }

	public void firePower()
    {
		FindObjectOfType<TemporaryCircleDeath>().fist = fireFist;
    }

	public void plaguePower()
    {
		FindObjectOfType<TemporaryCircleDeath>().fist = plagueFist;
    }
}