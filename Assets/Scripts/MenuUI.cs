using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{


	public GameObject shop;
	public GameObject menu;

	private void Start()
	{
#if UNITY_WEBGL
		YG.YandexGame.GetDataEvent += Data_Loaded;
#endif
	}

	private void Data_Loaded()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
		{
			menu.SetActive(true);
			shop.SetActive(false);
		}

//#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.H))
		{
			print("шта");
			Score.countBallsTotal += 100;
			//GameData.Instance.countBalls += 100;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			PlayerPrefs.DeleteAll();
			YG.YandexGame.savesData.gameData = new GameData();
			GameData.Instance.Save();
		}
		//#endif
	}
}
