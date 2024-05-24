using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {


	public void ShowPanel () {
		if (gameObject.activeSelf == false) {
			gameObject.SetActive (true); 
			Score.countBallsTotal += Score.countBalls;

			//$$$$$$$$$$$$$$$$$$$$
			Advertising.ShowAds();
			//$$$$$$$$$$$$$$$$$$$$
		}
	}
}
