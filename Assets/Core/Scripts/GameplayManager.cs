
using UnityEngine;

public class GameplayManager : MonoBehaviour
{


    #region Singleton

    public static GameplayManager instance;
    int fail=0;
    private void Awake()
    {
        instance = this;

    }
    #endregion


    //=====Variables=====//
    public static int levelIndex;
    [SerializeField] private Vector3 CarSpawnPos;
    [SerializeField] private Quaternion CarSpawnRotation;
    [SerializeField] public GameObject car;
    public GameObject level;
    public GameObject audioListener;
    public Camera rccCam;
    public LayerMask camMask;
    public float levelTimer;
    public static int RewindCount;
    public bool isLevelFailed;

    [SerializeField] private Material[] skybox;

    void Start()
    {
        
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
       
        RewindCount = 2;
        if (PlayerPrefs.HasKey("LevelIndex"))
        {
            levelIndex = PlayerPrefs.GetInt("LevelIndex");
            if (levelIndex > MainManager.TotalLevels)
            {
                levelIndex = MainManager.TotalLevels;
            }
        }
        else
        {
            levelIndex = 0;
        }


        level = Instantiate(MainManager.instance.levelholder.Levels[levelIndex].gameObject);
        car = MainManager.instance.carHolder.Cars[MainManager.instance.currentCarIndex].gameObject;
        
        RCC.SpawnRCC(car.GetComponent<RCC_CarControllerV3>(), CarSpawnPos, CarSpawnRotation,true, true, true);
        if (car.GetComponent<Rigidbody>())
        {
            car.GetComponent<Rigidbody>().isKinematic = false;
        }
        levelTimer = level.GetComponent<Level>().levelTimer;
        if(MainMenuUI.timeMode)
            TimeTrialTimer.Instance.StartTimer(levelTimer);
        AudioManager.instance.Stop("Theme2");
        AudioManager.instance.Play("Theme");
        //GAManager.instance.LevelProgressionEvent(levelIndex.ToString(), GAManager.LevelStatus.Start);

        //AdmobManager.instance.HideBanner(AdmobManager.BannerAD.Both);
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        RenderSettings.skybox = skybox[levelIndex];



       

    }


    public void NextLevel()
    {

        if (Time.timeScale < 1f)
            Time.timeScale = 1f;
      //  AdmobManager.instance.ShowInterstitialAd();
        AudioManager.instance.Play("Click");
        if (levelIndex < MainManager.instance.levelholder.Levels.Length - 1)
        {
            levelIndex++;
            PlayerPrefs.SetInt("LevelIndex", levelIndex);
            SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.Gameplay);
        }
        else
        {
            SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu);
        }
        //GameAnalytics.NewDesignEvent("Next_Level");

    }
    public void RestartLevel()
    {
        if (Time.timeScale < 1f)
            Time.timeScale = 1f;
        AudioManager.instance.Play("Click");
        PlayerPrefs.SetInt("LevelFailedCount", PlayerPrefs.GetInt("LevelFailedCount") + 1);
        if (PlayerPrefs.HasKey("LevelFailedCount") && PlayerPrefs.GetInt("LevelFailedCount") >= 2)
        {
           // AdmobManager.instance.ShowInterstitialAd(1.0f);
            PlayerPrefs.SetInt("LevelFailedCount", 0);
        }
        //GameAnalytics.NewDesignEvent("Restart_Level");
        SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.Gameplay);
    }

    public void LevelComplete()
    {
        if (isLevelFailed) return;

       
        //audioListener.SetActive(true);
        AudioManager.instance.Play("LevelComplete");
        TimeTrialTimer.Instance.StopTimer();
        //CameraChanger.instance.carCamera.cameraMode = RCC_Camera.CameraMode.CINEMATIC;
        rccCam.GetComponent<AudioListener>().enabled = false;
        rccCam.cullingMask = camMask;
        //CheckTimeReward();
       // AdmobManager.instance.ShowInterstitialAd(1.0f);
        Invoke(nameof(LevelCompleteFunctionsUI), 2f);
        //LevelCompleteFunctionsUI();

        DataSaver.instance.goldBars += TimeTrialTimer.goldBars;
        
    }

    public void LevelCompleteFunctionsUI()
    {

        if (levelIndex < MainManager.instance.levelholder.Levels.Length-1)
            MainManager.instance.levelholder.Levels[levelIndex + 1].isLevelUnlocked = true;
        MainManager.instance.levelholder.Levels[levelIndex].isLevelUnlocked = true;
        
        DataSaver.instance.cash += level.GetComponent<Level>().cashReward;
        //UIManager.instance.cashReward.text = level.GetComponent<Level>().cashReward.ToString();
        DataSaver.instance.SaveData();
       // AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.LargeBanner);
        UIManager.instance.OnLevelComplete(levelIndex);
        //GAManager.instance.LevelProgressionEvent(levelIndex.ToString(), GAManager.LevelStatus.Complete);
        //InHouseAds.Instance.Show(true);
    }

    private void CheckTimeReward()
    {
        float levelTimer = level.GetComponent<Level>().levelTimer;
        float elaspedTime = TimeTrialTimer.Instance.GetRemainingTime();
        float percentage = (elaspedTime / levelTimer) * 100;

        if (percentage > 0 && percentage < 50)
        {
            MainManager.instance.levelholder.Levels[levelIndex].position = 1;
        }
        else if (percentage >= 50 && percentage < 70)
        {
            MainManager.instance.levelholder.Levels[levelIndex].position = 2;
        }
        else
        {
            MainManager.instance.levelholder.Levels[levelIndex].position = 3;
        }

    }

    public void LevelFailed()
    {
        fail++;
        if (fail == 2)
           // AdmobManager.instance.ShowInterstitialAd(0.5f);
        isLevelFailed = true;
        rccCam.GetComponent<AudioListener>().enabled = false;
        //audioListener.SetActive(true);
        UIManager.instance.levelFailedCanvas.SetActive(true);
        UIManager.instance.rccCanvas.SetActive(false);
        UIManager.instance.OnLevelFailed();
        //InHouseAds.Instance.Show(true);
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.LargeBanner);
        AudioManager.instance.Play("LevelFailed");
        //GAManager.instance.LevelProgressionEvent(levelIndex.ToString(), GAManager.LevelStatus.Failed);
        DataSaver.instance.goldBars += TimeTrialTimer.goldBars;
        DataSaver.instance.SaveData();
    }
    public void PauseGame()
    {
       
        rccCam.GetComponent<AudioListener>().enabled = false;
        //audioListener.SetActive(true);
        UIManager.instance.levelPauseCanvas.SetActive(true);
        UIManager.instance.rccCanvas.SetActive(false);
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.LargeBanner);
        //InHouseAds.Instance.Show(true);
        AudioManager.instance.Play("Click");
        Time.timeScale = 0f;
        //GAManager.instance.CustomDesignEvent("GamePause");
    }
    public void ResumeGame()
    {
        //AdmobManager.instance.ShowInterstitialAd(0.0f);
        rccCam.GetComponent<AudioListener>().enabled = true;
        //audioListener.SetActive(false);
        Time.timeScale = 1f;
        UIManager.instance.levelPauseCanvas.SetActive(false);
        UIManager.instance.rccCanvas.SetActive(true);
        //AudioManager.instance.Play("Click");
        //InHouseAds.Instance.Show(false);
        //AdmobManager.instance.HideBanner(AdmobManager.BannerAD.LargeBanner);
        //GAManager.instance.CustomDesignEvent("GameResume");
    }
    public void GoToHome()
    {
        if (Time.timeScale < 1f)
            Time.timeScale = 1f;
        //AdmobManager.instance.ShowInterstitialAd(1.0f);
        // GAManager.instance.CustomDesignEvent("MainMenu");
        UIManager.instance.levelPauseCanvas.SetActive(false);

        SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu);
    }

}
