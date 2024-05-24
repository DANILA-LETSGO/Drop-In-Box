using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdateView : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = Score.countBallsTotal.ToString ();
	}
}
