﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidStats : MonoBehaviour {

    private bool displayStats = false;
    Vector3 screenPosition;

    [Range(0.0f, 10.0f)]
    public float size;          //fitness
    [Range(0.0f, 10.0f)]
    public float wealth;        //Hats
    [Range(0.0f, 10.0f)]
    public float heatlh;        //desease
    //[Range(0.0f, 10.0f)]
    public Color color;

    public bool hasBred = false; 

    [SerializeField]
    private MeshRenderer coloredRegion;
    public GameObject boidPrefab;
    public Transform hatPlace;
	public GameObject hatInst;

    public GameObject lovedOne;
    public GameObject breedingParticles;

    public bool squished = false;

    void OnMouseOver()
    {
        displayStats = true;
    }
    void OnMouseExit()
    {
        displayStats = false;
    }

    void OnGUI()
    {
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;

        if (displayStats == true)
        {
            GUI.Box(new Rect(screenPosition.x, screenPosition.y + 25, 110, 50), "Size: " + size + "\nWealth: " + wealth + "\nHealth: " + heatlh);
        }
    }

    public void generateStats()
    {
        size = Random.Range(0.0f, 10.0f);
        wealth = Random.Range(0.0f, 10.0f);
        heatlh = Random.Range(0.0f, 10.0f);
		//color = new Color (Utils.Map (heatlh, 0.0f, 10.0f, 0.0f, 0.42f), 0.42f, Utils.Map (heatlh, 0.0f, 10.0f, 0.0f, 0.42f));
        color = Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
        coloredRegion.material.color = color;
    }

    public void ApplyStatsVisuals()
    {
        coloredRegion.material.color = color;
        float mappedScale = Utils.Map(size, 0.0f, 10.0f, 0.5f, 1.5f);
        transform.localScale = new Vector3(mappedScale, mappedScale, mappedScale);
        hatPlace.localPosition = new Vector3(0.0f, .5f, 0.0f);
		//color = new Color (Utils.Map (heatlh, 0.0f, 10.0f, 0.0f, 0.42f), 0.42f, Utils.Map (heatlh, 0.0f, 10.0f, 0.0f, 0.42f));
    }

    public void hatSelect()
    {
        string hatLocation = "Hats/";
        if (wealth < 2.0f)
            hatLocation += "None";
        else if(wealth >= 2.0f && wealth < 4.0f)
            hatLocation += "Beanie";
        else if (wealth >= 4.0f && wealth < 6.0f)
            hatLocation += "Cap";
        else if (wealth >= 6.0f && wealth < 8.0f)
            hatLocation += "Bowler";
        else
            hatLocation += "TopHat";

        GameObject hat = Resources.Load(hatLocation) as GameObject;
        hatInst = Instantiate(hat, hatPlace.position, hatPlace.rotation, transform);
    }

	public void removeHat()
	{
		Destroy (hatInst);
	}

    // Use this for initialization
    void Awake() {
        generateStats();
        ApplyStatsVisuals();
        hatSelect();

    }

    // Update is called once per frame
    void Update() {

    }

    public GameObject findClosestZombot()
    {
        GameObject closestZombot = null;
        float dist = float.MaxValue;
        foreach (var zombot in RobotZombieBehaviour.Instance.robotZombies)
        {
            BoidStats zombotBS = zombot.GetComponent<BoidStats>();
            if (zombotBS != this)
            {
                float currDist = Vector3.Distance(transform.position, zombot.transform.position);
                if (currDist < dist)
                {
                    dist = currDist;
                    closestZombot = zombot;
                }
            }
        }
        return closestZombot;
    }

    public static GameObject breed(BoidStats boid1, BoidStats boid2, GameObject boidPrefab)
    {
        Vector3 spawnLoc = (boid1.transform.position + boid2.transform.position) / 2;
        spawnLoc.y = RobotZombieBehaviour.Instance.spawnHeight;
        GameObject newBorn = Instantiate(boidPrefab, spawnLoc, Quaternion.identity);
        GameObject parts = Instantiate(boid1.breedingParticles, spawnLoc, boid1.breedingParticles.transform.rotation);

        var nbStats = newBorn.GetComponent<BoidStats>();
		nbStats.size = Mathf.Max(0.0f, Mathf.Min(10.0f, ((boid1.size + boid2.size) / 2) + Utils.NextGaussian()));
		nbStats.wealth = Mathf.Max(0.0f, Mathf.Min(10.0f, ((boid1.wealth + boid2.wealth) / 2) + Utils.NextGaussian()));
		nbStats.heatlh = Mathf.Max(0.0f, Mathf.Min(10.0f, ((boid1.heatlh + boid2.heatlh) / 2) + Utils.NextGaussian()));
		nbStats.color.r = Mathf.Max(0.0f, Mathf.Min(1.0f, ((boid1.color.r + boid2.color.r) / 2) + Utils.NextGaussian()/10));
		nbStats.color.g = Mathf.Max(0.0f, Mathf.Min(1.0f, ((boid1.color.g + boid2.color.g) / 2) + Utils.NextGaussian()/10));
		nbStats.color.b = Mathf.Max(0.0f, Mathf.Min(1.0f, ((boid1.color.b + boid2.color.b) / 2) + Utils.NextGaussian()/10));
        nbStats.StartCoroutine(nbStats.babyTime(nbStats, nbStats.size));
        nbStats.removeHat();
        nbStats.ApplyStatsVisuals();
        nbStats.hatSelect();
        return newBorn;
    }

    IEnumerator babyTime(BoidStats newBorn, float size)
    {
        Timer timer = new Timer(1.0f);
        while (!timer.Trigger())
        {
            newBorn.size =  size / Utils.Map(timer.timeLeft, 0.0f, 1.0f, 1.0f, 5.0f);
            newBorn.ApplyStatsVisuals();
            yield return new WaitForSeconds(0.1f);
        }
        newBorn.size = size;
        newBorn.ApplyStatsVisuals();
        yield return null;
    }

    public IEnumerator squash(GameObject obj)
    {
        obj.transform.localScale = new Vector3(obj.transform.localScale.x, 0.01f, obj.transform.localScale.z);
        obj.transform.position = new Vector3(obj.transform.position.x, 0.01f, obj.transform.position.z);
        obj.transform.localRotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        squished = true;

        yield return new WaitForSeconds(3.0f);
        //obj.SetActive(false);
        yield return null;
    }
}
