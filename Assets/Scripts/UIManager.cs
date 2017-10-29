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

    // Use this for initialization
    void Start()
    {
		wealthSliderGO.minValue = 0;
		sizeSliderGO.minValue = 0;
		populationSliderGO.minValue = 0;

		wealthSliderGO.maxValue = 100;
		sizeSliderGO.maxValue = 100;
		populationSliderGO.maxValue = 100;

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

    // Update is called once per frame
    void Update()
    {
		//numberOfSpawned = (zombieMngr.numZombos - getNum.numSquished);//zombieMngr.numZombos;
		updateAlive ();
        updateSliders();
        generationCounter();
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

        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempWealth += rz.GetComponent<BoidStats>().wealth;
			}
        }

		averageWealth = (numberOfSpawned / tempWealth);

		wealthSliderGO.value = (averageWealth * 100);
    }

    void populationSlider()
    {
        float tempPopulation = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempPopulation += rz.GetComponent<BoidStats>().heatlh;
			}
        }

		averagePopulation = (numberOfSpawned / tempPopulation);

		populationSliderGO.value = (averagePopulation * 100); 
    }

    void sizeSlider()
    {
        float tempSize = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
			if (rz.GetComponent<BoidStats> ().squished == false) {
				tempSize += rz.GetComponent<BoidStats>().size;
			}
        }
		averageSize = (numberOfSpawned / tempSize);

		sizeSliderGO.value = (averageSize * 100);
    }

    void objectives()
    {
        objectivesText.text = "Objective: \n\n" +
			"" + objectivesList[Random.Range(0,11)]; 
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