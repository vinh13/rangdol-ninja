using System.Collections;
using UnityEngine;
using System;

public class adsManager : MonoBehaviour
{
    public static adsManager Instance;
    Action a;
    float timeinter;
    private void Awake()
    {
        timeinter = 0;
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    private void OnEnable()
    {
        IronSource.Agent.init("13bd63771", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);

        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;

    }
    private void Start()
    {

        loadInterstitial();
    }
    void InitBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

    }
    void loadInterstitial()
    {
        IronSource.Agent.loadInterstitial();
    }
    public void showInterstitial()
    {
        if (true && PlayerPrefs.GetInt(Purchaser.removeAds, 0)==0)
        {
            IronSource.Agent.showInterstitial();
            StartCoroutine("timeShowInter");
        }

    }
    public void showReward(Action val)
    {
        if (PlayerPrefs.GetInt(keysave.removeads, 0) == 0)
        {
            bool available = IronSource.Agent.isRewardedVideoAvailable();
            if (available)
            {
                a = val;
                IronSource.Agent.showRewardedVideo();

            }
        }

    }
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        a.Invoke();
        timeinter = 0;
        StopCoroutine("timeShowInter");
        StartCoroutine("timeShowInter");
    }
    void InterstitialAdShowSucceededEvent()
    {
    }
    IEnumerator timeShowInter()
    {
        if (timeinter <= 0)
        {
            while (timeinter < 30)
            {
                timeinter += Time.deltaTime;
                yield return null;
            }
            timeinter = 0;
        }
    }

}
