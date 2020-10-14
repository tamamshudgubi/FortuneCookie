using System;
using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;


public class WatchAdd : MonoBehaviour
{
    private const string API_KEY = "469c81d9ee37a92ec777f677783b2ebb3b4f9b4d0e0f581b";

    private void Start()
    {
        InitializeAdvertisment(true);
    }

    private void InitializeAdvertisment(bool isTesting)
    {
        Appodeal.setTesting(isTesting);
        Appodeal.disableLocationPermissionCheck();
        Appodeal.disableWriteExternalStoragePermissionCheck();
        Appodeal.initialize(API_KEY, Appodeal.REWARDED_VIDEO);
    }

    private void SendWatchRewardedVideoReport()
    {
        Dictionary<string, object> eventParameters = new Dictionary<string, object>();
        eventParameters.Add("AdNetwork ", "Appodeal");
        eventParameters.Add("Placement  ", "rewardedVideo");
        AppMetrica.Instance.ReportEvent("ShowViewAd", eventParameters);
    }

    public void OnWatchAddButtonClick()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }

        PlayerPrefs.SetString("Time", TimeSpan.Zero.ToString());
        SendWatchRewardedVideoReport();
    }
}
