using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public GameObject ball;
	public float speed = 0.8f;
	public bool isClicked;
	public BoxType boxType;
	public int countClicks;
	public bool nominated;
	public Score score;
	int multiplierCountBallToIncrease = 1;

	public static int countConsecutiveCaught = 0;
	static Box currentCatchingBox;
	static bool flameIsActive;

	public static int countConsecutiveCaught_2 = 0;
	static Box currentCatchingBox_2;

	// Use this for initialization
	void Start () {
		BonusManager.LoadData();
		countConsecutiveCaught = 0;
		flameIsActive = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isClicked == true && boxType == BoxType.Right && countClicks == 1) {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0, 0);
		}
		if (isClicked == true && boxType == BoxType.Left && countClicks == 1) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
		}
		if (isClicked == true && boxType == BoxType.Right && countClicks == 2) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			nominated = false;
		}
		if (isClicked == true && boxType == BoxType.Left && countClicks == 2) {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0, 0);
			nominated = false;
		}

	}

	void OnMouseDown () {
		if (Time.timeScale > 0.00001f) {
			Box[] boxes = FindObjectsOfType<Box> ();
			int countBoxes = boxes.Length;
			for (int i = 0; i < countBoxes; i++) {
				if (boxes [i].nominated == true) {
					boxes [i].isClicked = true;
					boxes [i].countClicks = 2;
				}
			}
			isClicked = true;
			nominated = true;
			countClicks = countClicks + 1;
			//Debug.Break ();
			GetComponents<AudioSource> () [1].Play ();
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Left Stoper" && boxType == BoxType.Right) {
			isClicked = false;
			countClicks = 1;
		}
		if (col.name == "Right Stoper" && boxType == BoxType.Left) {
			isClicked = false;
			countClicks = 1;
		}
		if (col.name == "Left Stoper 2" && boxType == BoxType.Left) {
			isClicked = false;
			countClicks = 0;
		}
		if (col.name == "Right Stoper 2" && boxType == BoxType.Right) {
			isClicked = false;
			//transform.position = new Vector3 (7.5f, transform.position.y, 0);
			countClicks = 0;
		}
		if ((col.name == "Ball" && GetComponent<SpriteRenderer> ().color == col.GetComponent<SpriteRenderer> ().color && col.GetComponent<Ball> ().isBonusBall == false) 
			|| (col.name == "Ball" && col.GetComponent<Ball> ().isBonusBall == true)
			|| (col.name == "Ball" && flameIsActive)) {
			GameObject effectPrefab = col.GetComponent<Ball> ().effectAddedScore;
			GameObject effectClone = Instantiate (effectPrefab);
			effectClone.GetComponent<EffectAddedScore> ().spawnPosition = col.transform.position;

			GameObject effectHidePrefab = col.GetComponent<Ball> ().effectHideBall;
			GameObject effectHideClone = Instantiate (effectHidePrefab, col.transform.position, Quaternion.identity);
			effectHideClone.GetComponent<SpriteRenderer> ().color = GetComponent<SpriteRenderer> ().color;

				
			int countBallsToIncrease = col.GetComponent<Ball> ().countBallsToIncreaseScore;
			int addedScore = (1 + LevelManager.GetTypeLevel()) + (Score.countBalls / countBallsToIncrease);


			#region BONUS +1
			if (BonusManager.isOpenBonus != null) {
				if (BonusManager.isOpenBonus [1] == true) {
					addedScore++;
				}
			}
			#endregion
			Score.countBalls = Score.countBalls + addedScore;
			effectClone.GetComponent<EffectAddedScore> ().addedValue = addedScore;
			Destroy (col.gameObject);

			GetComponents<AudioSource> () [0].Play ();

			#region BONUS FLAME
			if (BonusManager.isOpenBonus [0] == true) {// Если бонус доступен
				if(LevelManager.GetTypeLevel() == 2){// Если мы находимся на 3-ем типе уровня, с двумя шарами
					if(boxType == BoxType.Left){// Тогда отдельно считаем левые и правые шары 
						if((currentCatchingBox == this || currentCatchingBox == null) && col.GetComponent<Ball> ().isBonusBall == false){
							countConsecutiveCaught++;
							if(countConsecutiveCaught > 2 && flameIsActive == false){
								StartCoroutine(FlameBoxes());// Вызов Корутины, функция-таймер
							}
						} else {
							countConsecutiveCaught = 1;
						}
						currentCatchingBox = this;
					} else {
						if((currentCatchingBox_2 == this || currentCatchingBox_2 == null) && col.GetComponent<Ball> ().isBonusBall == false){
							countConsecutiveCaught_2++;
							if(countConsecutiveCaught_2 > 2 && flameIsActive == false){
								StartCoroutine(FlameBoxes());
							}
						} else {
							countConsecutiveCaught_2 = 1;
						}
						currentCatchingBox_2 = this;
					}
				} else {// Случай если мы находимся на отсальных типах уровней
					if((currentCatchingBox == this || currentCatchingBox == null) && col.GetComponent<Ball> ().isBonusBall == false){
						countConsecutiveCaught++;
						if(countConsecutiveCaught > 2 && flameIsActive == false){
							StartCoroutine(FlameBoxes());
						}
					} else {
						countConsecutiveCaught = 1;
					}
					currentCatchingBox = this;
				}
			}
			#endregion
		}

	}

	// Если нужно, то я опишу и объясню, что здесь происходит
	// а так тут процесс протекания Бонуса Пламени.
	IEnumerator FlameBoxes(){
		flameIsActive = true;
		GameObject flameEffect = Resources.Load<GameObject> ("FlameEffect");
		foreach (Box b in FindObjectsOfType<Box>()) {
			GameObject cloneEffect = Instantiate (flameEffect, b.transform);
			if (b.boxType == BoxType.Left) {
				cloneEffect.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}
		yield return new WaitForSeconds (1.8f);
		for (float f = 1; f > 0; f -= 0.1f) {
			foreach (Box b in FindObjectsOfType<Box>()) {
				ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient (new Color (1, 1, 1, f));
				var mainModule = b.transform.GetChild (0).GetChild (0).GetComponent<ParticleSystem> ().main;
				mainModule.startColor = color;
				b.transform.GetChild (0).GetChild (1).GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, f);
			}
			yield return new WaitForSeconds (0.88f);
		}
		yield return new WaitForSeconds (0.88f);
		countConsecutiveCaught = 0;
		flameIsActive = false;
		foreach (Box b in FindObjectsOfType<Box>()) {
			Destroy (b.transform.GetChild (0).gameObject);
		}
	}

	public enum BoxType {
		Left,
		Right
	}
}
	
