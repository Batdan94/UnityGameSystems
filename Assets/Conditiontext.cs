using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Conditiontext : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		text.text = FindObjectOfType<UIManager> ().objectivesText.text;	
	}
	
	// Update is called once per frame
	void Update () {
		updateText ();
	}

	void updateText()
	{
		text.text = FindObjectOfType<UIManager> ().objectivesText.text;	
	}

	public void loadMainMneu()
	{
		SceneManager.Loadscene (0);
	}
}
