using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidStats))]
public class DisplayBoidStats : MonoBehaviour {

	BoidStats bs;

	public float change1 = 0.1f;
	public float change2;
	public float change3;

	bool sizeUp;
	bool wealthUp;
	bool healthUp;
	void Start()
	{
		bs = GetComponent<BoidStats> ();
		change2 = change1 + 0.01f;
		change3 = change1 + 0.02f;

	}
	
	// Update is called once per frame
	void Update () {
		bs.heatlh += healthUp ? change1 : -change1;
		if (bs.heatlh >= 10.0f || bs.heatlh <= 0.0f) {
			healthUp = !healthUp;
		}

		bs.size += sizeUp ? change2 : -change2;
		if (bs.size >= 10.0f || bs.size <= 0.0f) {
			sizeUp = !sizeUp;
		}

		bs.wealth += wealthUp ? change3 : -change3;
		if (bs.wealth >= 10.0f || bs.wealth <= 0.0f) {
			wealthUp = !wealthUp;
		}

		bs.ApplyStatsVisuals ();
		bs.removeHat ();
		bs.hatSelect ();
	}
}
