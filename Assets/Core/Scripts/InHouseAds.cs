using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseAds : MonoBehaviour
{
    public static InHouseAds Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private string url;

    public void OnInHouseAdsClick()
    {
        Application.OpenURL(url);
    }
    public void Show(bool showCondition)
    {
        gameObject.SetActive(showCondition);
        if (showCondition)
        {
            //AdmobManager.instance.HideBanner(AdmobManager.BannerAD.SmallBanner);
        }
        else 
        { 
            //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        }
    }
}
