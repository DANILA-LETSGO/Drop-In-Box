using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour {

	public AudioClip hitsound;

	void OnCollisionEnter2D (Collision2D Col) {
		if (Col.gameObject.name == "Ball") {
			//gameObject.GetComponent<AudioSource> ().clip = hitsound;
			gameObject.GetComponent<AudioSource> ().Play ();
		}
	}
}
