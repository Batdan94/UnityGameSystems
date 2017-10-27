using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //gameobjects references
    public GameObject playerFist;

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

    //manager references
    public RobotZombieBehaviour zombieMngr;
    private GameManager gameMngr;
	public BoidStats getNum;

    //objectives list
    private Text[] objectivesList;

    // Use this for initialization
    void Start()
    {
		wealthSliderGO.minValue = 0;
		sizeSliderGO.minValue = 0;
		populationSliderGO.minValue = 0;

		wealthSliderGO.maxValue = 100;
		sizeSliderGO.maxValue = 100;
		populationSliderGO.maxValue = 100;

		numberOfSpawned = zombieMngr.numZombos;

        objectivesList = new Text[12];

        objectivesList[0].text = "Have a 100% wealthy population";
        objectivesList[1].text = "MaHaveke a 100% poor population";

        objectivesList[2].text = "Have a 100% large population";
        objectivesList[3].text = "Have a 100% small population";

        objectivesList[4].text = "Have a wealthy zombie population";
        objectivesList[5].text = "Have a wealthy robot population";

        objectivesList[6].text = "Have a poor zombie population";
        objectivesList[7].text = "Have a poor robot population";

        objectivesList[8].text = "Have a large zombie population";
        objectivesList[9].text = "Have a large robot population";

        objectivesList[10].text = "Have a small zombie population";
        objectivesList[11].text = "Have a small robot population";
    }

    // Update is called once per frame
    void Update()
    {
		numberOfSpawned = zombieMngr.numZombos;
        updateSliders();
        generationCounter();
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

		averageWealth = ((numberOfSpawned - getNum.numSquished) / tempWealth);

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

		averagePopulation = ((numberOfSpawned - getNum.numSquished) / tempPopulation);

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
		averageSize = ((numberOfSpawned - getNum.numSquished) / tempSize);

		sizeSliderGO.value = (averageSize * 100);
    }

    void objectives()
    {
        objectivesText.text = "Objective: " + objectivesList[(int)Random.value];
    }

    void generationCounter()
    {
        roundText.GetComponent<Text>().text = "Gen Num: " + GameManager.Instance.roundNumber;
    }

    void punchPower()
    {
        playerFist = fist;
    }

    void LightningPower()
    {
        playerFist = lightningFist;
    }

    void fireBallPower()
    {
        playerFist = fireFist;
    }

    void bioHazardPower()
    {
        playerFist = plagueFist;
    }
}