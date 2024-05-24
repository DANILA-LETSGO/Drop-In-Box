using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    public Sprite[] sprites;
    public float delay = 3f;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(delay);
        int countSprites = sprites.Length;

        transform.GetChild(0).GetComponent<Image>().sprite = sprites[Random.Range(0, countSprites)];
        Color randomColor = new Color(Random.Range(0.58f, 1f), Random.Range(0.58f, 1f), Random.Range(0.58f, 1f), 0f);
        transform.GetChild(0).GetComponent<Image>().color = randomColor;

        for (float f = 0; f < 1f; f += 0.001f) {
            yield return null;
            randomColor.a = f;
            transform.GetChild(0).GetComponent<Image>().color = randomColor;
        }
        GetComponent<Image>().color = randomColor;
        GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        yield return new WaitForSeconds(delay);
        StartCoroutine(Start());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
