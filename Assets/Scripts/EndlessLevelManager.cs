using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelManager : MonoBehaviour {

	public float timeSwitchColor;
	public Color[] colorsBox;
	public int[] randomIndexColors;

	// Use this for initialization
	void Start () {
		StartCoroutine (SwitchColor ());
	}
	
	IEnumerator SwitchColor(){
		randomIndexColors = new int[transform.childCount];

		for (int i = 0; i < randomIndexColors.Length; i++) {
			int randomIndex = Random.Range (0, colorsBox.Length);
			bool indexNotFound = false;
			while (indexNotFound == false) {
				randomIndex = Random.Range (0, colorsBox.Length);
				yield return null;
				for (int j = 0; j < randomIndexColors.Length; j++) {
					if (randomIndexColors [j] == randomIndex) {
						indexNotFound = false;
						break;
					} else {
						indexNotFound = true;
					}
				}
				//if (indexNotFound == true) {
					
				//}
			}
			randomIndexColors [i] = randomIndex;
		}
		yield return new WaitForSeconds (timeSwitchColor);
		while (FindObjectOfType<Ball> () != null) {
			yield return null;
			print ("ждем пока сука шаров не будет");
		}
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).GetComponent<SpriteRenderer>().color = colorsBox[randomIndexColors[i]];
		}
		StartCoroutine (SwitchColor ());
	}
}
