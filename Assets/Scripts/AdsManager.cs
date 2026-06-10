using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    public Button TriggerAdButton;

    private bool mobileAdsInitialized;
    private AdRequest adRequest;

    private void Awake()
    {
        TriggerAdButton.onClick.AddListener(OnAdButtonClicked);
    }

    private void Start()
    {
        MobileAds.Initialize(status =>
        {
            mobileAdsInitialized = true;
            Debug.Log($"MobileAds ## Initialized! {status}");
        });
    }

    private void OnAdButtonClicked()
    {
        if (!mobileAdsInitialized)
        {
            Debug.LogError("Mobile Ads not initialized");
            return;
        }

        if (adRequest != null)
        {
            Debug.LogError("Ad Request is progress");
            return;
        }
        
        adRequest = new AdRequest();
        
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log(error.ToString());
                adRequest = null;
                return;
            }

            if (ad.CanShowAd())
            {
                ad.Show((reward =>
                {
                    Debug.Log($"MobileAds ## Rewarded: {reward}");
                    adRequest = null;
                }));
            }
            else
            {
                adRequest = null;
            }
        });
    }
}