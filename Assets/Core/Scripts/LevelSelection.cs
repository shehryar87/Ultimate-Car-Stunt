using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void OnClickLevelButton(int level)
    {
        level-= 1;
        AudioManager.instance.Play("Click");
        PlayerPrefs.SetInt("LevelIndex", level);
       // AdmobManager.instance.ShowInterstitialAd();
        gameObject.SetActive(false);
       // AdmobManager.instance.HideBanner(AdmobManager.BannerAD.SmallBanner);
        MainMenuUI.instance.garageCanvas.SetActive(true);
        //SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.Gameplay);
    }

}
