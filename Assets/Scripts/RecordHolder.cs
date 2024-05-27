using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class RecordHolder : MonoBehaviour {

    public static int RecordType_1
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        get
        {
            if (!PlayerPrefs.HasKey("record_1"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("record_1");
        }
        set
        {
            PlayerPrefs.SetInt("record_1", value);
            PlayerPrefs.Save();
        }
#endif

#if UNITY_WEBGL
        get
        {
            if (GameData.Instance != null)
                return GameData.Instance.recordType_1;
            else
                return 0;
        }
        set
        {
            GameData.Instance.recordType_1 = value;
            GameData.Instance.Save();
        }
#endif
    }

    public static int RecordType_2
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        get
        {
            if (!PlayerPrefs.HasKey("record_2"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("record_2");
        }
        set
        {
            PlayerPrefs.SetInt("record_2", value);
            PlayerPrefs.Save();
        }
#endif

#if UNITY_WEBGL
        get
        {
            if (GameData.Instance != null)
                return GameData.Instance.recordType_2;
            else
                return 0;
        }
        set
        {
            GameData.Instance.recordType_2 = value;
            GameData.Instance.Save();
        }
#endif
    }

    public static int RecordType_3
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        get
        {
            if (!PlayerPrefs.HasKey("record_3"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("record_3");
        }
        set
        {
            PlayerPrefs.SetInt("record_3", value);
            PlayerPrefs.Save();
        }
#endif

#if UNITY_WEBGL
        get
        {
            if (GameData.Instance != null)
                return GameData.Instance.recordType_3;
            else
                return 0;
        }
        set
        {
            GameData.Instance.recordType_3 = value;
            GameData.Instance.Save();
        }
#endif
    }

    public static int RecordType_Endless
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        get
        {
            if (!PlayerPrefs.HasKey("record_endless"))
            {
                return 0;
            }
            return PlayerPrefs.GetInt("record_endless");
        }
        set
        {
            PlayerPrefs.SetInt("record_endless", value);
            PlayerPrefs.Save();
        }
#endif

#if UNITY_WEBGL
        get
        {
            if (GameData.Instance != null)
                return GameData.Instance.recordType_Endless;
            else
                return 0;
        }
        set
        {
            GameData.Instance.recordType_Endless = value;
            GameData.Instance.Save();
        }
#endif

    }

    Text txtRecord;
    int currentRecord;
	int indexMap;
	string sceneName;

	void Awake () {
		txtRecord = transform.GetChild(1).GetComponent<Text>();
	}

    void Start()
    {
		sceneName = SceneManager.GetActiveScene ().name;
		if (sceneName != "Level Endless")
        {
			indexMap = int.Parse(sceneName.Replace("Level ", ""));
            if (indexMap <= 5 )
            {
                currentRecord = RecordType_1;
            }
            else if (indexMap <= 10)
            {
                currentRecord = RecordType_2;
            }
            else
            {
                currentRecord = RecordType_3;
            }
        }
        else {
            currentRecord = RecordType_Endless;
        }
           
    }
   

    void Update () {
        txtRecord.text = currentRecord.ToString();

	}


    private void OnDisable()
    {
        if(Score.countBalls > currentRecord)
        {
			if (SceneManager.GetActiveScene ().name != "Level Endless") {
				int indexMap = int.Parse (SceneManager.GetActiveScene ().name.Replace ("Level ", ""));
				if (indexMap <= 5) {
					RecordType_1 = Score.countBalls;
				} else if (indexMap <= 10) {
					RecordType_2 = Score.countBalls;
				} else {
					RecordType_3 = Score.countBalls;
				}
			} else {
				RecordType_Endless = Score.countBalls;
			}

#if UNITY_WEBGL
            var currentScore = Score.countBalls;

            List<int> records = new List<int>()
            {
                RecordType_1,
                RecordType_2,
                RecordType_3,
                RecordType_Endless
            };

            var maxValue = records.Max();
            if (maxValue < currentScore)
            {
                YG.YandexGame.NewLeaderboardScores("maxScore", currentScore);
            }
#endif
        }


    }
}
