using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnLevel : MonoBehaviour {

	public Text txt_Price;
	public Image iconLock;
	public Image iconLevel;
	
	public void OnClick(int levelNumber){
		FindObjectOfType<LevelManager> ().SelectLevel (levelNumber);
	}
}
