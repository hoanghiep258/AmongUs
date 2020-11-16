using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    public string APP_ID = "ca-app-pub-5366914889955115~8768911034";
    public string banner_ID = "ca-app-pub-3940256099942544/6300978111";
    public string interstitial_ID = "ca-app-pub-3940256099942544/4411468910";
    public string video_ID = "ca-app-pub-3940256099942544/1712485313";
    private BannerView bannerAD;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardVideoAd;

    private Action interstitalActionCallback;
    private Action videoActionCallback;
    private bool isStart = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        MobileAds.Initialize(APP_ID);
        RequestBanner();
        RequestInterstitial();
        RequestVideoAd();
    }

    IEnumerator Start()
    {
        int count = PlayerPrefs.GetInt("CountStart", 0);
        if (count > 0)
        {
            DisplayInterstitialAD();
            RequestInterstitial();
            count++;
            PlayerPrefs.SetInt("CountStart", count);
        }
        yield return new WaitForSecondsRealtime(20);
        isStart = true;
    }

    private void OnEnable()
    {
        HandleBannerADEvents(true);
        HandleInterstitialADEvents(true);
        HandleRewardADEvents(true);
    }

    private void OnDisable()
    {
        HandleBannerADEvents(false);
        HandleInterstitialADEvents(false);
        HandleRewardADEvents(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    private void RequestBanner()
    {
        // test
        //string banner_ID = "ca-app-pub-3940256099942544/6300978111";
        // real
        //string banner_ID = "ca-app-pub-5366914889955115/8194195965";
        bannerAD = new BannerView(banner_ID, AdSize.SmartBanner, AdPosition.Top);

        // real
        //AdRequest adRequest = new AdRequest.Builder().Build();        

        // test
        AdRequest adRequest = new AdRequest.Builder()            
            .Build();

        bannerAD.LoadAd(CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        // test
        //string interstitial_ID = "ca-app-pub-3940256099942544/4411468910";
        // real
        //string interstitial_ID = "ca-app-pub-5366914889955115/1792439148";
        interstitialAd = new InterstitialAd(interstitial_ID);
        Debug.LogError("RequestInterstitial");
        // real
        //AdRequest adRequest = new AdRequest.Builder().Build();
        this.interstitialAd.OnAdClosed -= this.HandleOnInterstitialAdClosed;
        this.interstitialAd.OnAdClosed += this.HandleOnInterstitialAdClosed;
        //test
        AdRequest adRequest = new AdRequest.Builder()           
           .Build();

        interstitialAd.LoadAd(CreateAdRequest());
    }


    public void RequestVideoAd()
    {
        // test
        //string video_ID = "ca-app-pub-3940256099942544/1712485313";
        // real
        //string video_ID = "ca-app-pub-5366914889955115/7974704110";        
        this.rewardVideoAd = new RewardedAd(video_ID);
        // real
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //test
        this.rewardVideoAd.OnUserEarnedReward -= HandleUserEarnedReward;
        this.rewardVideoAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest adRequest = new AdRequest.Builder()           
           .Build();
       
        rewardVideoAd.LoadAd(adRequest);
    }

    public void DisplayBanner()
    {
        bannerAD.Show();
    }

    public void HideBanner()
    {
        bannerAD.Hide();
    }

    public void DisplayInterstitialAD(Action callback = null)
    {
        interstitalActionCallback = null;

        if (interstitialAd.IsLoaded())
        {
            Debug.LogError("DisplayInterstitialAD");
            if (callback != null)
            {
                interstitalActionCallback = callback;
            }
            interstitialAd.Show();
        }
        else
        {
            
            if (callback != null)
            {
                callback();
            }
        }
    }

    public void DisplayRewardAD(Action callback = null)
    {
        videoActionCallback = null;
        if (rewardVideoAd.IsLoaded())
        {
            if (callback != null)
            {
                videoActionCallback = callback;
            }
            rewardVideoAd.Show();
        }
    }

    #region Banner

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLoaded event received");
        DisplayBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLeavingApplication event received");
    }

    private void HandleBannerADEvents(bool subcribe)
    {
        if (subcribe)
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }
        
    }
    #endregion

    #region Interstitital

    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLoaded event received");
        DisplayInterstitialAD();
    }

    public void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("HandleOnInterstitialAd Failed To Load");        
    }

    public void HandleOnInterstitialAdOpened(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdOpened event received");
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdClosed event received");
        if (interstitalActionCallback != null)
        {
            interstitalActionCallback();            
        }        
    }

    public void HandleOnInterstitialAdLeavingApplication(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLeavingApplication event received");
    }

    private void HandleInterstitialADEvents(bool subcribe)
    {
        if (subcribe)
        {
            // Called when an ad request has successfully loaded.
            this.interstitialAd.OnAdLoaded += this.HandleOnInterstitialAdLoaded;
            // Called when an ad request failed to load.
            this.interstitialAd.OnAdFailedToLoad += this.HandleOnInterstitialAdFailedToLoad;
            // Called when an ad is clicked.
            this.interstitialAd.OnAdOpening += this.HandleOnInterstitialAdOpened;
            // Called when the user returned from the app after an ad click.
            this.interstitialAd.OnAdClosed += this.HandleOnInterstitialAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstitialAd.OnAdLeavingApplication += this.HandleOnInterstitialAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.interstitialAd.OnAdLoaded -= this.HandleOnInterstitialAdLoaded;
            // Called when an ad request failed to load.
            this.interstitialAd.OnAdFailedToLoad -= this.HandleOnInterstitialAdFailedToLoad;
            // Called when an ad is clicked.
            this.interstitialAd.OnAdOpening -= this.HandleOnInterstitialAdOpened;
            // Called when the user returned from the app after an ad click.
            this.interstitialAd.OnAdClosed -= this.HandleOnInterstitialAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstitialAd.OnAdLeavingApplication -= this.HandleOnInterstitialAdLeavingApplication;
        }

    }
    #endregion

    #region Rewards

    public void HandleOnVideoRewardAdLoaded(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLoaded event received");
        DisplayRewardAD();
        
    }

    public void HandleOnVideoRewardAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
    }

    public void HandleOnVideoRewardAdOpened(object sender, EventArgs args)
    {        
         Debug.LogError("HandleAdOpened event received");
    }

    public void HandleOnVideoRewardAdClosed(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdClosed event received");        
        if (videoActionCallback != null)
        {
            videoActionCallback();
        }
    }

    public void HandleOnVideoRewardAdLeavingApplication(object sender, EventArgs args)
    {
         Debug.LogError("HandleAdLeavingApplication event received");
       
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
         Debug.LogError("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
         Debug.LogError(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
         Debug.LogError("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
         Debug.LogError(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.LogError("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.LogError(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        if (videoActionCallback != null)
        {
            videoActionCallback();
            videoActionCallback = null;
        }
    }
    private void HandleRewardADEvents(bool subcribe)
    {
        if (subcribe)
        {

            // Called when an ad request has successfully loaded.
            this.rewardVideoAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            this.rewardVideoAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this.rewardVideoAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this.rewardVideoAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardVideoAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardVideoAd.OnAdClosed += HandleRewardedAdClosed;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.rewardVideoAd.OnAdLoaded -= HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            this.rewardVideoAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this.rewardVideoAd.OnAdOpening -= HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this.rewardVideoAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardVideoAd.OnUserEarnedReward -= HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardVideoAd.OnAdClosed -= HandleRewardedAdClosed;
        }

    }
    #endregion
    private void OnApplicationPause(bool pause)
    {
        if (pause && isStart)
        {
            int count = PlayerPrefs.GetInt("CountStart", 0);
            if (count > 0)
            {
                count++;
                PlayerPrefs.SetInt("CountStart", count);
                DisplayInterstitialAD();
                RequestInterstitial();
            }  
            else
            {
                count++;
                PlayerPrefs.SetInt("CountStart", count);
            }
        }
    }
    private void OnApplicationQuit()
    {
        int count = PlayerPrefs.GetInt("CountStart", 0);
        count++;
        PlayerPrefs.SetInt("CountStart", count);
    }
}
