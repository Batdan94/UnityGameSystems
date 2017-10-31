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

    public AudioSource explainSource;
    public AudioClip explainClip;

    public BoidStats boid;

    //slider objects
    public Slider sizeSliderGO;
    public Slider wealthSliderGO;
    public Slider populationSliderGO;

    public TemporaryCircleDeath death;

    //temp floats
    float averageWealth;
    float averageSize;
    float averagePopulation;
    int numberOfSpawned;

    public Circle circle;

    Timer timer;
    int attackIndex = 3;

    //manager references
    private TemporaryCircleDeath getNum;

    // Use this for initialization
    void Start () {
        timer = new Timer(2.0f);
        NextAttack();
        explainSource = GetComponent<AudioSource>();
        explainSource.PlayOneShot(explainClip, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        updateSliders();
        if (timer.Trigger())
        {
            NextAttack();
        }

	}

    void updateSliders()
    {
        wealthSliderGO.value = (boid.wealth);
        sizeSliderGO.value = boid.size;
        populationSliderGO.value = boid.heatlh;
    }
    
    void NextAttack()
    {
        attackIndex++;
        if (attackIndex == 4)
        {
            attackIndex = 0;
        }

        switch(attackIndex)
        {
            case 0:
                circle.xradius = 3.0f;
                circle.zradius = 3.0f;
                circle.CreatePoints();
                death.fist = fist;
                break;
            case 1:
                circle.xradius = 1.0f;
                circle.zradius = 1.0f;
                circle.CreatePoints();
                death.fist = lightningFist;

                break;
            case 2:
                circle.xradius = 1.0f;
                circle.zradius = 1.0f;
                circle.CreatePoints();
                death.fist = fireFist;

                break;
            case 3:
                circle.xradius = 1.0f;
                circle.zradius = 1.0f;
                circle.CreatePoints();
                death.fist = plagueFist;

                break;
        }

        death.attackExp();
    }

}
