using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandlerAcessItems : MonoBehaviour {


	[SerializeField] GameObject txtCount;
	[SerializeField] Image areaCountAccess;


	void OnEnable () {
		StartCoroutine (DelayHandler ());
	}
	
	IEnumerator DelayHandler() {
		yield return null;
		int countAccessItems = 0;
		if (Score.countBallsTotal >= LevelManager.price * LevelManager.multiplierPrice) {
			countAccessItems++;
		}
		if (Score.countBallsTotal >= BonusManager.price) {
			countAccessItems++;
		}
		if (countAccessItems > 0) {
			areaCountAccess.enabled = true;
			txtCount.SetActive (true);
			txtCount.GetComponent<Text> ().text = countAccessItems.ToString ();
		} else {
			areaCountAccess.enabled = false;
			txtCount.SetActive (false);
		}

	}
}
