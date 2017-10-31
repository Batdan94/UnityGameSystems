﻿using System.Collections;
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

    //button objects
    public Button fistButton;
    public Button lightningButton;
    public Button fireButton;
    public Button plagueButton;

	private Button currentSelected;
	private Button tempButton1;
	private Button tempButton2;
	private Button tempButton3;

    //temp floats
    float averageWealth;
	float averageSize;
	float averagePopulation;
	int numberOfSpawned;

	public Circle circle;

    //manager references
    private RobotZombieBehaviour zombieMngr;
	private TemporaryCircleDeath getNum;

    //Timer
    int cooldownTimer;

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
        DontDestroyOnLoad(this);
		wealthSliderGO.minValue = 0;
		sizeSliderGO.minValue = 0;
		populationSliderGO.minValue = 0;

		wealthSliderGO.maxValue = 10;
		sizeSliderGO.maxValue = 10;
		populationSliderGO.maxValue = 10;

		zombieMngr = RobotZombieBehaviour.Instance;
		numberOfSpawned = zombieMngr.numZombos;

		cooldownTimer = 5;

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
		FindObjectOfType<TemporaryCircleDeath> ().fist = fist;
		FindObjectOfType<TemporaryCircleDeath> ().selectedAttack = 0;
		//FindObjectOfType<TemporaryCircleDeath> ().enabled = true;
		//circle.enabled = true;
    }

	public void LightningPower()
    {
		circle.xradius = 1.0f;
		circle.zradius = 1.0f;
		circle.CreatePoints();
		FindObjectOfType<TemporaryCircleDeath>().fist = lightningFist;
		FindObjectOfType<TemporaryCircleDeath> ().selectedAttack = 1;
		//FindObjectOfType<TemporaryCircleDeath> ().enabled = true;
		//circle.enabled = true;
    }

	public void firePower()
    {
		circle.xradius = 1.0f;
		circle.zradius = 1.0f;
		circle.CreatePoints();
		FindObjectOfType<TemporaryCircleDeath>().fist = fireFist;
		FindObjectOfType<TemporaryCircleDeath> ().selectedAttack = 2;
		//FindObjectOfType<TemporaryCircleDeath> ().enabled = true;
		//circle.enabled = true;
    }

	public void plaguePower()
    {
		circle.xradius = 1.0f;
		circle.zradius = 1.0f;
		circle.CreatePoints();
		FindObjectOfType<TemporaryCircleDeath>().fist = plagueFist;
		FindObjectOfType<TemporaryCircleDeath> ().selectedAttack = 3;
		//FindObjectOfType<TemporaryCircleDeath> ().enabled = true;
		//circle.enabled = true;
    }

	public IEnumerator buttonCooldown(Button buttonToCool)
	{
        Debug.Log("Calling Button Cooldown"); 
		if (buttonToCool == fistButton) 
		{
            FindObjectOfType<TemporaryCircleDeath>().hasSmashed = true;
        }
		else if (buttonToCool == lightningButton)
		{
            FindObjectOfType<TemporaryCircleDeath>().hasStruck = true;
        }
		else if (buttonToCool == fireButton) 
		{
            yield return new WaitForSeconds (1);
            FindObjectOfType<TemporaryCircleDeath>().hasLit = true;
        }
        else if (buttonToCool == plagueButton) 
		{
			FindObjectOfType<TemporaryCircleDeath>().hasPoisoned = true;
		}

		buttonToCool.interactable = false;
		//FindObjectOfType<TemporaryCircleDeath> ().enabled = false;
		//circle.enabled = false;
		yield return new WaitForSeconds (cooldownTimer);
        FindObjectOfType<TemporaryCircleDeath>().hasSmashed = false;
        FindObjectOfType<TemporaryCircleDeath>().hasStruck = false;
        FindObjectOfType<TemporaryCircleDeath>().hasLit = false;
        buttonToCool.interactable = true;
	}
}