using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectAddedScore : MonoBehaviour {

	public Vector3 spawnPosition;
	public float addedValue;
	public float maxRange = 38f;

	// Use this for initialization
	IEnumerator Start () {

		//spawnPosition.y += 1;
		spawnPosition = new Vector3(-2f,3f,0f);
		//Rect rect = GetComponent<RectTransform> ().rect;
		//rect.width = Screen.width / 8;
		//GetComponent<RectTransform> ().rect = rect;
		Vector2 rect = GetComponent<Text>().rectTransform.sizeDelta;
		rect.x = Screen.width / 7;
		GetComponent<Text> ().rectTransform.sizeDelta = rect;
		GetComponent<Text> ().text = "+" + addedValue.ToString ();
		GetComponent<Text> ().color = FindObjectOfType<Score> ().GetComponent<Text> ().color;

		transform.parent = FindObjectOfType<UI> ().transform;

		transform.position = Camera.main.WorldToScreenPoint (spawnPosition);


		//Vector2 dir = new Vector2 (Random.Range (-1f, 1f), Random.Range (0, 1f));
		Vector2 dir = new Vector2 (Random.Range (0f, 0.5f), Random.Range (-0.5f, 0.5f) ) * Screen.width/7;
		maxRange *= Screen.width/380;

		dir = new Vector2 (Mathf.Clamp (dir.x, -maxRange, maxRange), Mathf.Clamp (dir.y, -maxRange, maxRange));

		GetComponent<Rigidbody2D> ().AddForce (dir, ForceMode2D.Impulse);
		yield return new WaitForSeconds (1f);
		for (float f = 1; f > 0; f -= 0.01f) {
			Color color = GetComponent<Text> ().color;
			color.a = f;
			GetComponent<Text> ().color = color;
			yield return null;
		}
		Destroy (gameObject);
	}
	

}
