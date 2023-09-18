
using UnityEngine;
using UnityEngine.UI;

public class CrossPromotionAds : MonoBehaviour
{
    public AD[] houseAd;
    [SerializeField] private Image image;
    private string gameUrl;
    private string gameName;

    private void Start()
    {
        var ad = houseAd[Random.Range(0, houseAd.Length)];
        image.sprite = ad.adImage;
        gameUrl = ad.url;
        gameName = ad.gameName;
        

    }

    public void OpenURL()
    {
        
        Application.OpenURL(gameUrl);
        Time.timeScale = 1f;
        CrossPromotion.Instance.canvas.enabled = false;
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }


    public void CloseAd()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
        CrossPromotion.Instance.canvas.enabled = false;
        

    }
}

[System.Serializable]
public class AD
{
    public string gameName;
    public Sprite adImage;
    public string url;
}