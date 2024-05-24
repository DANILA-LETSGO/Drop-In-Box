using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnBonus : MonoBehaviour {

    public Text txt_Price;
    public GameObject iconBonus;
    public Image iconUsed;

    public void OnClick(int numberBonus) {
        FindObjectOfType<BonusManager>().SelectBonus(numberBonus);
    }
}
