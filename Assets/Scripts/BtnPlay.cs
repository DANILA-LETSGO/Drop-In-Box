using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnPlay : MonoBehaviour {


	public void OnButtonPlay () {
        string nameScene;
        if (LevelManager.indexLastPlayLevel == -1) {
			nameScene = "Level Endless";
		} else {
			nameScene = "Level " + (LevelManager.indexLastPlayLevel + 1).ToString ();
		}
		SceneManager.LoadScene (nameScene);
	}
}
