﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
		other.GetComponent<BoidStats> ().squished = true;
    }
}
