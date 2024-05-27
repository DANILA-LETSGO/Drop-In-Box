using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Score : MonoBehaviour
{
	public static int countBalls;
	public static int countBallsTotal
	{
		get
		{
#if UNITY_ANDROID || UNITY_STANDALONE
			if (!PlayerPrefs.HasKey("score"))
			{
				return 0;
			}
			return PlayerPrefs.GetInt("score");
#endif

#if UNITY_WEBGL
			if (GameData.Instance != null)
				return GameData.Instance.countBalls;
			else
				return 0;
#endif
		}
		set
		{
#if UNITY_ANDROID || UNITY_STANDALONE
			PlayerPrefs.SetInt("score", value);
			PlayerPrefs.Save();
#endif

#if UNITY_WEBGL
			GameData.Instance.countBalls = value;

			if (saveTimer > 30)
			{
				saveTimer = 0;

				GameData.Instance.Save();
			}
#endif
		}
	}

	public Text text;

	static float saveTimer;

	void Start()
	{
		countBalls = 0;
	}


	void Update()
	{
		saveTimer += Time.deltaTime;

		text.text = countBalls.ToString();
	}

    private void OnDestroy()
    {
		GameData.Instance.Save();
	}
}
