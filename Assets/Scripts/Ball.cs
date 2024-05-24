using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Color[] colors;
	public int probabilityBonusBall;
	public bool isBonusBall;
	public int countBallsToIncreaseScore = 50;
	public GameObject effectAddedScore;
	public GameObject effectHideBall;
	[Header("Ссылки на типы шаров")]
	public GameObject side_4;
	public GameObject side_6;
	public GameObject side_3;
	public AudioClip soundBonus; 
	// ==== не Публиги =====
	List<Color> leftColorSet = new List<Color>();
	List<Color> rigthColorSet = new List<Color>();


	// Use this for initialization
	void Start () {
		Box[] boxes = FindObjectsOfType<Box> ();
		int countElements = boxes.Length;
		colors = new Color[countElements];

		for (int i = 0; i < countElements; i++) {
			colors [i] = boxes [i].GetComponent<SpriteRenderer> ().color;
			if (boxes [i].boxType == Box.BoxType.Left) {
				leftColorSet.Add (boxes [i].GetComponent<SpriteRenderer> ().color);
			}
			if (boxes [i].boxType == Box.BoxType.Right) {
				rigthColorSet.Add (boxes [i].GetComponent<SpriteRenderer> ().color);
			}
		}
		if (Random.Range (0, 100) < probabilityBonusBall) {
			if (countElements == 4) {
				side_4.SetActive (true);
				for (int i = 0; i < countElements; i++) {
					side_4.transform.GetChild (i).GetComponent<SpriteRenderer> ().color = colors [i];
				}
			} else if (countElements == 6 && FindObjectsOfType<Spawn> ().Length == 1) {
				side_6.SetActive (true);
				for (int i = 0; i < countElements; i++) {
					side_6.transform.GetChild (i).GetComponent<SpriteRenderer> ().color = colors [i];
				}

			} else if (countElements == 6 && FindObjectsOfType<Spawn> ().Length == 2) {
				side_3.SetActive (true);
				for (int i = 0; i < side_3.transform.childCount; i++) {
					if (transform.position.x > 0) {
						side_3.transform.GetChild (i).GetComponent<SpriteRenderer> ().color = rigthColorSet [i];
					} else if (transform.position.x < 0) {
						side_3.transform.GetChild (i).GetComponent<SpriteRenderer> ().color = leftColorSet [i];
					}
				}
			}
			isBonusBall = true;
			GetComponent<Rigidbody2D> ().angularVelocity = 88;
			GetComponent<SpriteRenderer> ().enabled = false;
			SoundBonusBall ();
		} else {
			if (countElements == 6 && FindObjectsOfType<Spawn> ().Length == 2) {
				if (transform.position.x > 0) {
					int randomColor = Random.Range (0, rigthColorSet.Count);
					GetComponent<SpriteRenderer> ().color = rigthColorSet [randomColor];
				}
				if (transform.position.x < 0) {
					int randomColor = Random.Range (0, leftColorSet.Count);
					GetComponent<SpriteRenderer> ().color = leftColorSet [randomColor];
				}
			} else {
				int randomColor = Random.Range (0, countElements);
				GetComponent<SpriteRenderer> ().color = colors [randomColor];
			}
			isBonusBall = false;
		}
	}
	

	void SoundBonusBall () {
		gameObject.GetComponent<AudioSource> ().Play ();
	}
}
