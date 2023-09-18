
using UnityEngine;

public class CrossPromotion : MonoBehaviour
{
    public static CrossPromotion Instance;
    private void Awake()
    {
        Instance = this;
    }
    public CrossPromotionAds crossAd;
    public Canvas canvas;


    public void ShowCrossPromotion()
    {

        Instantiate(crossAd, transform);
        canvas.enabled = true;
        
        Time.timeScale = 0f;
        
    }
    
}
