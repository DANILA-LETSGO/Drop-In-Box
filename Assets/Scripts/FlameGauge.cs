using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameGauge : MonoBehaviour {


	void Update () {
		if (Box.countConsecutiveCaught > 2) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetChild (0).gameObject.SetActive (true);
				transform.GetChild (i).GetComponent<Image> ().color = new Color (1, 1, 1, 1);
			}
		} else if (Box.countConsecutiveCaught > 1) {
			transform.GetChild(0).GetComponent<Image> ().color = new Color (1, 1, 1, 0.3f);
			transform.GetChild(1).GetComponent<Image> ().color = new Color (1, 1, 1, 0.7f);
			transform.GetChild(2).GetComponent<Image> ().color = new Color (1, 1, 1, 1f);
		} else if (Box.countConsecutiveCaught > 0) {
			transform.GetChild(0).GetComponent<Image> ().color = new Color (1, 1, 1, 0.05f);
			transform.GetChild(1).GetComponent<Image> ().color = new Color (1, 1, 1, 0.25f);
			transform.GetChild(2).GetComponent<Image> ().color = new Color (1, 1, 1, 0.55f);

		} else {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetChild (0).gameObject.SetActive (false);
				transform.GetChild (i).GetComponent<Image> ().color = new Color (1, 1, 1, 0);
			}
		}
	}
}
