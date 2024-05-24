using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectorValueChange : MonoBehaviour {

    Text text;
    int valueTxt;
    public float maxSize = 1.5f;
    public float minSize = 0.8f;
    public float delayBetweenScales = 0.1f;
    public float timeIncrease = 0.1f;
    public float timeDecrease = 0.2f;
    Vector3 originScale;
    Coroutine corEffect;
    

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        valueTxt = int.Parse(text.text);
        originScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        int currentValue = int.Parse(text.text);
        if (currentValue != valueTxt) {
            if (corEffect != null)
                StopCoroutine(corEffect);
            corEffect = StartCoroutine(Effect());
            valueTxt = currentValue;
        }

        /*if (Input.GetKeyDown(KeyCode.G)) {
            text.text = (int.Parse(text.text) + 1).ToString();
        }/**/
	}

    IEnumerator Effect()
    {
        Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);
        Vector3 minScale = new Vector3(minSize, minSize, minSize);
        Vector3 velocity = Vector3.zero;
        while (transform.localScale.x < maxSize - 0.01f) {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, maxScale, ref velocity, timeIncrease);
            yield return null;
        }
        yield return new WaitForSeconds(delayBetweenScales);
        velocity = Vector3.zero;
        while (transform.localScale.x > minSize + 0.01f)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, minScale, ref velocity, timeDecrease);
            yield return null;
        }
        //yield return new WaitForSeconds(delayBetweenScales);
        velocity = Vector3.zero;

        while (transform.localScale.x < originScale.x - 0.01f)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, originScale, ref velocity, timeDecrease);
            yield return null;
        }
    }
}
