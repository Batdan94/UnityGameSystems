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

    //manager references
    RobotZombieBehaviour zombieMngr;
    GameManager gameMngr;

    //objectives list
    private Text[] objectivesList;

    // Use this for initialization
    void Start()
    {
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
           tempWealth += rz.GetComponent<BoidStats>().wealth;
        }

        wealthSliderGO.value = (tempWealth / zombieMngr.numZombos);
    }

    void populationSlider()
    {
        float tempPopulation = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
            tempPopulation += rz.GetComponent<BoidStats>().heatlh;
        }

        wealthSliderGO.value = (tempPopulation / zombieMngr.numZombos);
    }

    void sizeSlider()
    {
        float tempSize = 0;

        foreach (var rz in zombieMngr.robotZombies)
        {
            tempSize += rz.GetComponent<BoidStats>().size;
        }

        wealthSliderGO.value = (tempSize / zombieMngr.numZombos);
    }

    void objectives()
    {
        objectivesText = objectivesList[(int)Random.value];
    }

    void generationCounter()
    {
        roundText.GetComponent<Text>().text = "Generation Number: " + gameMngr.roundNumber;
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
