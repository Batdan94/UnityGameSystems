using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject playerFist;
    public GameObject roundText;
    public Slider sizeSliderGO;
    public Slider wealthSliderGO;
    public Slider populationSliderGO;
    RobotZombieBehaviour zombieMngr;
    GameManager gameMngr;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

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

    }

    void sizeSlider()
    {
        
    }

    void objectives()
    {

    }

    void generationCounter()
    {
        roundText.GetComponent<Text>().text = "Generation Number: " + gameMngr.roundNumber;
    }

    void punchPower()
    {

    }

    void LightningPower()
    {

    }

    void fireBallPower()
    {

    }

    void bioHazardPower()
    {

    }
}
