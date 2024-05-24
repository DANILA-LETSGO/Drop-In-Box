using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowPanelPause () {
		gameObject.SetActive (true);
		Time.timeScale = 0;
	}
}
