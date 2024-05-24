using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordHolder : MonoBehaviour {

    public static int recordType_1 {
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
    }
    public static int recordType_2 {
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
    }
    public static int recordType_3
    {
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
    }
    public static int recordType_Endless
    {
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
                currentRecord = recordType_1;
            }
            else if (indexMap <= 10)
            {
                currentRecord = recordType_2;
            }
            else
            {
                currentRecord = recordType_3;
            }
        }
        else {
            currentRecord = recordType_Endless;
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
					recordType_1 = Score.countBalls;
				} else if (indexMap <= 10) {
					recordType_2 = Score.countBalls;
				} else {
					recordType_3 = Score.countBalls;
				}
			} else {
				recordType_Endless = Score.countBalls;
			}
        }
    }
}
