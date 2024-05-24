using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawn : MonoBehaviour {

	public GameObject ball;
	public float timer;
	float timerOrigin;

	// Use this for initialization
	void Start () {
		timerOrigin = timer;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0) {
			GameObject ballClone;
			ballClone = Instantiate (ball, transform.position, Quaternion.identity);
			timer = timerOrigin;
			ballClone.name = "Main Ball";
		}
		timer -= Time.deltaTime;
	}
}
