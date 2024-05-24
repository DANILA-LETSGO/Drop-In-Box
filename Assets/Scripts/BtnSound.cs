using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour {


	void OnMouseUpAsDown () {
		GetComponent<AudioSource> ().Play ();
	}
}
