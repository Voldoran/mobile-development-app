using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    private const string RewardedAdIOS = "ca-app-pub-3940256099942544/1712485313";
    private const string RewardedAdAndroid = "ca-app-pub-3940256099942544/5224354917";
    
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
            Debug.LogError("Ad Request is in progress");
            return;
        }
        
        adRequest = new AdRequest();

        string unitId = string.Empty;
        
#if UNITY_ANDROID
        unitId = RewardedAdAndroid;
#elif UNITY_IOS
        unitId = RewardedAdIOS;
#endif
        
        RewardedAd.Load(unitId, adRequest, (RewardedAd ad, LoadAdError error) =>
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