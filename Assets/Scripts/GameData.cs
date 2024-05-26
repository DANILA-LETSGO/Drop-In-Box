using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public int countBalls;
    public int indexLastPlayedLevel;
    public int multiplierPrice;
    public int recordType_1;
    public int recordType_2;
    public int recordType_3;
    public int recordType_Endless;
    public bool isOpenEndless;
    public bool isAccessEndless;
    public List<bool> isOpenLevels = new List<bool>();
    public List<bool> isAccessLevels = new List<bool>();

   
    public GameData()
    {
        multiplierPrice = 1;
    }

    public static GameData Instance
    {
        get
        {
            if (YG.YandexGame.savesData.gameData == null)
            {
                YG.YandexGame.savesData.gameData = new GameData();
            }

            return YG.YandexGame.savesData.gameData;
        }
        

    }

    public void Save()
    {
#if UNITY_WEBGL
        YG.YandexGame.savesData.gameData = this;

        YG.YandexGame.SaveProgress();
#endif
    }
}
