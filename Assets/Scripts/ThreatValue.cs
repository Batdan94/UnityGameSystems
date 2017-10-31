using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreatValue : MonoBehaviour {

	public float threat = 2.0f;

	// Use this for initialization
	void Awake () {
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("BoidsScene"))
            GameManager.Instance.threats.Add(this);
	}

	// Update is called once per frame
	void Update () {
		threat -= Time.deltaTime;
		if (threat <= 0)
			Destroy (this.gameObject);
	}
}
