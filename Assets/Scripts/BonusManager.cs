using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public int originPrice = 8000;
    public BtnBonus[] btnBonuses;
    public static bool[] isOpenBonus;
    public static int price = 2500;

    public static int multiplierPrice
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        get
        {
            if (!PlayerPrefs.HasKey("multiplierB"))
            {
                return 1;
            }
            return PlayerPrefs.GetInt("multiplierB");
        }
        set
        {
            PlayerPrefs.SetInt("multiplierB", value);
            PlayerPrefs.Save();
        }
#endif

#if UNITY_WEBGL
        get
        {
            return GameData.Instance.multiplierBonus;
        }
        set
        {
            GameData.Instance.multiplierBonus = value;
            GameData.Instance.Save();
        }
#endif
    }

    public void Init()
    {
        price = originPrice;
        isOpenBonus = new bool[btnBonuses.Length];
        LoadData();
        UpdateBonusView();
    }

    void UpdateBonusView()
    {
        for (int i = 0; i < btnBonuses.Length; i++)
        {
            if (isOpenBonus[i] == true)
            {
                btnBonuses[i].iconUsed.gameObject.SetActive(true);
                btnBonuses[i].iconBonus.SetActive(true);
                btnBonuses[i].txt_Price.gameObject.SetActive(false);
            }
            else
            {
                btnBonuses[i].iconUsed.gameObject.SetActive(false);
                btnBonuses[i].iconBonus.SetActive(false);
                btnBonuses[i].txt_Price.gameObject.SetActive(true);
                btnBonuses[i].txt_Price.text = (originPrice * multiplierPrice).ToString();
            }
        }
    }

    public void SelectBonus(int numberBonus)
    {
        if (isOpenBonus[numberBonus] == false && Score.countBallsTotal >= originPrice * multiplierPrice)
        {
            isOpenBonus[numberBonus] = true;

            Score.countBallsTotal -= originPrice;
            multiplierPrice++;
        }
        SaveData();
        UpdateBonusView();
    }

    public static void LoadData()
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        isOpenBonus = new bool[2];//new bool[itemsBonuses.Length]; TODO
		if (!PlayerPrefs.HasKey ("bonuses")) {
			
			return;
		}
        string strBonuses = PlayerPrefs.GetString("bonuses");
        if (strBonuses.Length == 0)
            return;
        string[] itemsBonuses = strBonuses.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        

        for (int i = 0; i < itemsBonuses.Length; i++)
        {
            isOpenBonus[i] = bool.Parse(itemsBonuses[i]);
        }
#endif

#if UNITY_WEBGL
        var data = GameData.Instance;
        //isOpenBonus = new bool[data.isOpenBonus.Count];
        for (int i = 0; i < data.isOpenBonus.Count; i++)
        {
            isOpenBonus[i] = data.isOpenBonus[i];
        }
#endif

    }

    public static void SaveData()
    {
#if UNITY_ANDROID || UNITY_STANDALONE
        string saveBonuses = "";

        for (int i = 0; i < isOpenBonus.Length; i++)
        {
            saveBonuses += string.Format("{0};", isOpenBonus[i]);
        }

        PlayerPrefs.SetString("bonuses", saveBonuses);
        PlayerPrefs.Save();
#endif

#if UNITY_WEBGL
        GameData.Instance.isOpenBonus = new List<bool>();
        for (int i = 0; i < isOpenBonus.Length; i++)
        {
            GameData.Instance.isOpenBonus.Add(isOpenBonus[i]);
        }
        GameData.Instance.Save();
#endif
    }
}
