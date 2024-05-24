using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	//public List<BtnLevel> btnLevels = new List<BtnLevel>();
	public BtnLevel[] btnLevels;
	public BtnLevel btnEndless;
	public int originPrice;
	public static int price = 250;
	public static bool[] isOpenLevels;
	public static bool[] isAccessLevels;
	public Text txt_Score;

	public static int multiplierPrice {
		get { 
			if (!PlayerPrefs.HasKey ("multiplier")) {
				return 1;
			}
			return PlayerPrefs.GetInt ("multiplier");
		}
		set { 
			PlayerPrefs.SetInt ("multiplier", value);
			PlayerPrefs.Save ();
		}
	}

	public static bool isOpenEndless {
		get { 
			if (!PlayerPrefs.HasKey ("openEndless")) {
				return false;
			}
			return bool.Parse(PlayerPrefs.GetString ("openEndless"));
		}
		set { 
			PlayerPrefs.SetString ("openEndless", value.ToString());
			PlayerPrefs.Save ();
		}
	}
	public static bool isAccessEndless {
		get { 
			if (!PlayerPrefs.HasKey ("accessEndless")) {
				return false;
			}
			return bool.Parse(PlayerPrefs.GetString ("accessEndless"));
		}
		set { 
			PlayerPrefs.SetString ("accessEndless", value.ToString());
			PlayerPrefs.Save ();
		}
	}


	public static int indexLastPlayLevel {
		get { 
			if (!PlayerPrefs.HasKey ("lastLevel")) {
				return 0;
			}
			return PlayerPrefs.GetInt ("lastLevel");
		}
		set { 
			PlayerPrefs.SetInt ("lastLevel", value);
			PlayerPrefs.Save ();
		}
	}
	//public static LevelManager instance;


	void Start () {
		//btnLevels = FindObjectsOfType<BtnLevel> ();
		//instance = this;
		price = originPrice;

		isOpenLevels = new bool[btnLevels.Length];
		isAccessLevels = new bool[btnLevels.Length]; 
		isOpenLevels [0] = true;
		//----------------------------
		isAccessLevels [0] = true;
		isAccessLevels [1] = true;
		isAccessLevels [2] = true;
		isAccessLevels [3] = true;
		isAccessLevels [4] = true;
		//---------------------------
		LoadData();
		UpdateViewLevels();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			Score.countBallsTotal = 88888;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			//Score.countBallsTotal = 0;
			PlayerPrefs.DeleteAll ();
			//UpdateViewLevels ();
		}
	}

	public void UpdateViewLevels(){
		for (int i = 0; i < btnLevels.Length; i++) {
			if (isAccessLevels [i] == true) {
				btnLevels [i].iconLock.gameObject.SetActive (false);
				if (isOpenLevels [i] == true) {
					btnLevels [i].txt_Price.gameObject.SetActive (false);
					btnLevels [i].iconLevel.enabled = true;
				} else {
					btnLevels [i].txt_Price.gameObject.SetActive (true);
				}
			}
			btnLevels [i].txt_Price.text = (originPrice * multiplierPrice).ToString ();
		}
		if (isAccessEndless == true) {
			btnEndless.iconLock.gameObject.SetActive (false);
			btnEndless.txt_Price.gameObject.SetActive (true);
			btnEndless.txt_Price.text = (originPrice * (multiplierPrice*2)).ToString();
			if (isOpenEndless == true) {
				btnEndless.txt_Price.gameObject.SetActive (false);
				btnEndless.iconLevel.enabled = true;
			}
		}
		SaveData ();
	}

	public void SelectLevel(int levelNumber){
		if (levelNumber == -1) {
			
			if (isAccessEndless == true && Score.countBallsTotal >= originPrice * (multiplierPrice * 2) && isOpenEndless == false) {
				isOpenEndless = true;
				Score.countBallsTotal -= originPrice * (multiplierPrice * 2);
			} else if (isOpenEndless == true) {
				indexLastPlayLevel = levelNumber;
				SceneManager.LoadScene ("Level Endless");
			}
		} else {
			if (isOpenLevels [levelNumber] == false && Score.countBallsTotal >= originPrice * multiplierPrice) {
				Score.countBallsTotal -= originPrice * multiplierPrice;
				isOpenLevels [levelNumber] = true;
				multiplierPrice += 1;//TODO

				for (int i = 0; i < 3; i++) {
					bool isPurchasedAll = false;
					for (int j = 0; j < 5; j++) {
						if (isOpenLevels [(i * 5) + j] == true) {
							isPurchasedAll = true;
						} else {
							isPurchasedAll = false;
							break;
						}
					}
					if (isPurchasedAll == true) {
						int indexFirstLevel = 5 * (i + 1);
						if (indexFirstLevel < btnLevels.Length) {
							isOpenLevels [indexFirstLevel] = true;

							for (int j = 0; j < 5; j++) {
								isAccessLevels [((i + 1) * 5) + j] = true;
							}
						}
					}
				}
				// Проверяем открыты ли все уровни
				// И если все уровни открыты, то мы открываем доступ к бесконечному уровню
				bool isOpenAllLevel = true;
				for (int i = 0; i < isOpenLevels.Length; i++) {
					if (isOpenLevels [i]) {
						continue;
					} else {
						isOpenAllLevel = false;
						break;
					}
				}
				if (isOpenAllLevel == true) {
					isAccessEndless = true;
				}
				//------------------------------------------------------------------------

			} else if (isOpenLevels [levelNumber] == true) {
				string nameLevel = "Level " + (levelNumber + 1).ToString ();
				indexLastPlayLevel = levelNumber;
				SceneManager.LoadScene (nameLevel);
			}
		}
		UpdateViewLevels ();
	}


	public static int GetTypeLevel(){
		string nameLevel = SceneManager.GetActiveScene ().name;
		if (nameLevel != "Level Endless") {
			int indexMap = int.Parse(nameLevel.Replace("Level ", ""));
			return (indexMap-1) / 5;
		} else
			return 0;
	}


	public static void LoadData(){
		if (!PlayerPrefs.HasKey ("levels"))
			return;
		string strLevels = PlayerPrefs.GetString ("levels");
		if (strLevels.Length == 0)
			return;
		string[] itemsLevels = strLevels.Split (new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
		//string[] itemsCompleted = strCompl.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < itemsLevels.Length; i++) {
			isOpenLevels [i] = bool.Parse (itemsLevels [i]);
		}

		string strAccess = PlayerPrefs.GetString ("access");

		string[] itemsAccess = strAccess.Split (new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
		//string[] itemsCompleted = strCompl.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < itemsAccess.Length; i++) {
			isAccessLevels [i] = bool.Parse (itemsAccess [i]);
		}
	}

	public static void SaveData(){
		string saveLevel = "";
		string saveAccess = "";
		for (int i = 0; i < isOpenLevels.Length; i++) {
			saveLevel += string.Format ("{0};", isOpenLevels [i]);
		}
		for (int i = 0; i < isAccessLevels.Length; i++) {
			saveAccess += string.Format ("{0};", isAccessLevels [i]);
		}

		PlayerPrefs.SetString("levels", saveLevel);
		PlayerPrefs.SetString ("access", saveAccess);

		PlayerPrefs.Save ();
	}
}
