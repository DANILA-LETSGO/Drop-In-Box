using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
#endif

public class Advertising : MonoBehaviour
{

	public static int countGameSessions = 0;

	void Start()
	{
#if UNITY_ANDROID
		Appodeal.disableNetwork("inmobi");
		Appodeal.disableNetwork("yandex");

		string appKey = "cd707a851f14d4e06523665599677e0ff03e2926dc4e2a7c";
		Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO);
#endif
	}


	public static void ShowAds()
	{
		countGameSessions++;
		if (countGameSessions > 1)
		{
#if UNITY_ANDROID
			if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
			{
				Appodeal.show(Appodeal.REWARDED_VIDEO);
			}
			else if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO))
			{
				Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
			}
			else if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
			{
				Appodeal.show(Appodeal.INTERSTITIAL);
			}
			else
			{
				Appodeal.show(Appodeal.REWARDED_VIDEO);
			}
#endif
			countGameSessions = 0;
		}

	}
}
