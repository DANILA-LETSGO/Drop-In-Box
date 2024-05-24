using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static int countBalls;
	public static int countBallsTotal {
		get { 
			if (!PlayerPrefs.HasKey ("score")) {
				return 0;
			}
			return PlayerPrefs.GetInt ("score");
		}
		set { 
			PlayerPrefs.SetInt ("score", value);
			PlayerPrefs.Save ();
		}
	}
	public Text text;

	// Use this for initialization
	void Start () {
		countBalls = 0;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = countBalls.ToString ();
	}
}
