﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Main Ball") {
			Destroy (col.gameObject);
		}
	}
}
