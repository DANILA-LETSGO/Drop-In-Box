using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject ball;
	public float timer;
	public float timer_2;
	public float timerReductionStep;
	public float timerLimit;
	float timerOrigin;
	float timerOrigin_2;

	// Use this for initialization
	void Start () {
		timerOrigin = timer;
		timerOrigin_2 = timer_2;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < 0 && timer_2 < 0) {
			GameObject ballClone;
			ballClone = Instantiate (ball, transform.position, Quaternion.identity);
			timer = timerOrigin;
			timer_2 = timerOrigin;
			ballClone.name = "Ball";
			if (timerOrigin > timerLimit) {
				timerOrigin = timerOrigin - timerReductionStep;
			}
		}
		timer -= Time.deltaTime;
		timer_2 -= Time.deltaTime;
	}	


	void OnTriggerEnter2D(Collider2D col){
		
	}


	public void HideSpawn () {
		gameObject.SetActive (false); 
	}
}
