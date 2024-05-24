using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Advertising : MonoBehaviour {

	public static int countGameSessions = 0;

	void Start () {
		Appodeal.disableNetwork("inmobi");
		Appodeal.disableNetwork("yandex");

		string appKey = "cd707a851f14d4e06523665599677e0ff03e2926dc4e2a7c";
		Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO);
	}

	public static void ShowAds(){
		countGameSessions++;
		if (countGameSessions > 1) {
			if (Appodeal.isLoaded (Appodeal.REWARDED_VIDEO)) {
				Appodeal.show (Appodeal.REWARDED_VIDEO);
			} else if (Appodeal.isLoaded (Appodeal.NON_SKIPPABLE_VIDEO)) {
				Appodeal.show (Appodeal.NON_SKIPPABLE_VIDEO);
			} else if (Appodeal.isLoaded (Appodeal.INTERSTITIAL)) {
				Appodeal.show (Appodeal.INTERSTITIAL);
			} else {
				Appodeal.show (Appodeal.REWARDED_VIDEO);
			}
			countGameSessions = 0;
		}

	}

}
