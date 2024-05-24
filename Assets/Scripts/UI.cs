using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public PanelPause panelPause;

	// Use this for initialization
	void Start () {
		BonusManager.LoadData ();
		if (BonusManager.isOpenBonus [0] == true) {
			Instantiate (Resources.Load<GameObject> ("Flame Gauge"), transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			panelPause.ShowPanelPause ();
		}
	}
}
