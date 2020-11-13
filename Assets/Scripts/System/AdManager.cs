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
    private RewardBasedVideoAd rewardVideoAd;

    private Action interstitalActionCallback;
    private Action videoActionCallback;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        MobileAds.Initialize(APP_ID);
        RequestBanner();
        RequestInterstitial();
        RequestVideoAd();
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
            .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
            .Build();

        bannerAD.LoadAd(adRequest);
    }

    private void RequestInterstitial()
    {
        // test
        //string interstitial_ID = "ca-app-pub-3940256099942544/4411468910";
        // real
        //string interstitial_ID = "ca-app-pub-5366914889955115/1792439148";
        interstitialAd = new InterstitialAd(interstitial_ID);
        Debug.LogError("RequestInterstitial");
        // real
        //AdRequest adRequest = new AdRequest.Builder().Build();
       
        //test
       AdRequest adRequest = new AdRequest.Builder()
           .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
           .Build();

        interstitialAd.LoadAd(adRequest);
    }


    private void RequestVideoAd()
    {
        // test
        //string video_ID = "ca-app-pub-3940256099942544/1712485313";
        // real
        //string video_ID = "ca-app-pub-5366914889955115/7974704110";
        rewardVideoAd = RewardBasedVideoAd.Instance;
      
        // real
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //test
        AdRequest adRequest = new AdRequest.Builder()
           .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")
           .Build();

        rewardVideoAd.LoadAd(adRequest, video_ID);
    }

    public void DisplayBanner()
    {
        bannerAD.Show();
    }

    public void DisplayInterstitialAD(Action callback = null)
    {
        interstitalActionCallback = null;
        Debug.LogError("DisplayInterstitialAD");
        if (interstitialAd.IsLoaded())
        {
            if (callback != null)
            {
                interstitalActionCallback = callback;
            }
            interstitialAd.Show();
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
        MonoBehaviour.print("HandleAdLoaded event received");
        DisplayBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
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
        MonoBehaviour.print("HandleAdLoaded event received");
        DisplayInterstitialAD();
    }

    public void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("HandleOnInterstitialAd Failed To Load");
        RequestInterstitial();
    }

    public void HandleOnInterstitialAdOpened(object sender, EventArgs args)
    {
        RequestInterstitial();
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestInterstitial();
        if (interstitalActionCallback != null)
        {
            interstitalActionCallback();            
        }
    }

    public void HandleOnInterstitialAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
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
        MonoBehaviour.print("HandleAdLoaded event received");
        DisplayRewardAD();
        
    }

    public void HandleOnVideoRewardAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestVideoAd();
    }

    public void HandleOnVideoRewardAdOpened(object sender, EventArgs args)
    {
        RequestVideoAd();
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnVideoRewardAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestVideoAd();
        if (videoActionCallback != null)
        {
            videoActionCallback();
        }
    }

    public void HandleOnVideoRewardAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
       
    }

    private void HandleRewardADEvents(bool subcribe)
    {
        if (subcribe)
        {
            // Called when an ad request has successfully loaded.
            this.rewardVideoAd.OnAdLoaded += this.HandleOnVideoRewardAdLoaded;
            // Called when an ad request failed to load.
            this.rewardVideoAd.OnAdFailedToLoad += this.HandleOnVideoRewardAdFailedToLoad;
            // Called when an ad is clicked.
            this.rewardVideoAd.OnAdOpening += this.HandleOnVideoRewardAdOpened;
            // Called when the user returned from the app after an ad click.
            this.rewardVideoAd.OnAdClosed += this.HandleOnVideoRewardAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.rewardVideoAd.OnAdLeavingApplication += this.HandleOnVideoRewardAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.rewardVideoAd.OnAdLoaded -= this.HandleOnVideoRewardAdLoaded;
            // Called when an ad request failed to load.
            this.rewardVideoAd.OnAdFailedToLoad -= this.HandleOnVideoRewardAdFailedToLoad;
            // Called when an ad is clicked.
            this.rewardVideoAd.OnAdOpening -= this.HandleOnVideoRewardAdOpened;
            // Called when the user returned from the app after an ad click.
            this.rewardVideoAd.OnAdClosed -= this.HandleOnVideoRewardAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.rewardVideoAd.OnAdLeavingApplication -= this.HandleOnVideoRewardAdLeavingApplication;
        }

    }
    #endregion
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            DisplayInterstitialAD();
        }
    }
}
