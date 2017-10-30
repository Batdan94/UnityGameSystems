using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationSceneController : MonoBehaviour {
    //gameobjects references
    public GameObject fist;
    public GameObject lightningFist;
    public GameObject fireFist;
    public GameObject plagueFist;

    public BoidStats boid;

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
    private TemporaryCircleDeath getNum;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        updateSliders();
        

	}

    void updateSliders()
    {
        wealthSliderGO.value = (boid.wealth);
        sizeSliderGO.value = boid.size;
        populationSliderGO.value = boid.heatlh;
    }
    
}
