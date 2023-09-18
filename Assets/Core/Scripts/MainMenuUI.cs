
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI instance;
    [SerializeField] private Button NextCarBtn, PreviousCarBtn, PlayBtn, ExitBtn, SettingBtn, ShopBtn;
    [SerializeField] public TextMeshProUGUI carNameText, cashText, rewardCountText;
    [SerializeField] public Image speedImg, accelImg, handlingImg, lockImg;
    [SerializeField] private GameObject currentCar;
    [SerializeField] private GameObject PreviousCar;
    [SerializeField] private Vector3 showPos;
    [SerializeField] private Quaternion showRot;
    [SerializeField] public GameObject GarageCam, LevelSelectionCam, LevelSelectionCanvas, mainMenuCanvas, modeCanvas;
    [SerializeField] private float maxCarSpeed;
    [SerializeField] private InHouseAds inHouse;
    [SerializeField] public GameObject garageCanvas, garageCam;
    [SerializeField] private Image carNameImage;
    [SerializeField] private GameObject unlockBtn;
    public Sprite position1;
    public Sprite position2;
    public Sprite position3;
    public GameObject lockedCarText;

    public GameObject rewardedAdCanvas;

    private int lockCarIndex;
   
    [SerializeField] private TextMeshProUGUI goldBarsTxt;
     public static bool timeMode;
    private void Start()
    {

        
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        Application.targetFrameRate = 30;
        instance = this;
     //   PlayBtn.onClick.AddListener(OnPlayButtonClick);
        if (PlayerPrefs.HasKey("CarIndex"))
            MainManager.instance.currentCarIndex = PlayerPrefs.GetInt("CarIndex");
        else
            MainManager.instance.currentCarIndex = 0;

        currentCar = Instantiate(MainManager.instance.carHolder.Cars[MainManager.instance.currentCarIndex].gameObject, showPos, showRot);
        /*currentCar.GetComponent<Rigidbody>().useGravity = false;*/
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Theme2");
     //   cashText.text = DataSaver.instance.cash.ToString();
     

        goldBarsTxt.text =  DataSaver.instance.goldBars.ToString();
        /* try
         {

             if (AdmobManager.instance != null)
                 AdmobManager.instance.HideBanner(AdmobManager.BannerAD.Both);
         }
         catch (Exception ex)
         {
             Debug.Log(ex);
         }
         inHouse.Show(false);*/
        if (PlayerPrefs.GetInt("FirstTime") != 1)
        {

            PlayerPrefs.SetInt("FirstTime", 1);
            SceneLoader.instance.GoToGameplay();
        }

        //    CheckCarIsUpgraded();
        //    CheckCarIsUnlocked();
        
    }
  

    public void OnPlayButtonClick()
    {
        AudioManager.instance.Play("Click");
        PlayerPrefs.SetInt("CarIndex", MainManager.instance.currentCarIndex);
        GarageCam.SetActive(false);
        mainMenuCanvas.SetActive(false);
        //LevelSelectionCam.SetActive(true);
       
        LevelSelectionCanvas.SetActive(true);
        //inHouse.Show(false);
        //GameAnalytics.NewDesignEvent("Play_Button_Click");

    }

    public void GaragePlayBtn()
    {
        AudioManager.instance.Play("Click");
        GarageCam.SetActive(false);
        mainMenuCanvas.SetActive(false);
        LevelSelectionCanvas.SetActive(false);
        modeCanvas.SetActive(true);
    }
   

    public void PlayGame( bool timeModeSelection)
    {
        //AdmobManager.instance.ShowInterstitialAd(1.0f);
        AudioManager.instance.Play("Click");
        PlayerPrefs.SetInt("CarIndex", MainManager.instance.currentCarIndex);
        GarageCam.SetActive(false);
        SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.Gameplay);
        timeMode = timeModeSelection;
    }
    public void OnBackButtonClick()
    {
        AudioManager.instance.Play("Click");
        AudioManager.instance.Play("Theme2");
        PlayerPrefs.SetInt("CarIndex", MainManager.instance.currentCarIndex);
        GarageCam.SetActive(true);
        //LevelSelectionCam.SetActive(false);
        mainMenuCanvas.SetActive(true);
        LevelSelectionCanvas.SetActive(false);
        //garageCam.SetActive(false);
        garageCanvas.SetActive(false);
        OnCarSelection(MainManager.instance.currentCarIndex);

    }

    private int carIndex = 0;
    [SerializeField] private int totalCars = 3;
    public void NextCar()
    {
        if (carIndex < totalCars - 1)
        {

            carIndex++;
        }
        else
        {
            carIndex = 0;
        }
        OnCarSelection(carIndex);
        /*currentCar.GetComponent<Rigidbody>().useGravity = false;*/


        /* Debug.Log("goldBars " + DataSaver.instance.goldBars, gameObject);
         Debug.Log("carprice " + currentCar.GetComponent<Car>().carDataHolder.carPrice, gameObject);
         Debug.Log("lockCarIndex " + lockCarIndex, gameObject);*/
    }
    public void LastCar()
    {
        if (carIndex <= 0)
        {
            carIndex = totalCars - 1;
        }
        else
        {
            carIndex--;

        }
      /*  currentCar.GetComponent<Rigidbody>().useGravity = false;*/
        OnCarSelection(carIndex);
    }

    public void OnCarSelection(int carIndex)
    {

        PreviousCar = currentCar;
        AudioManager.instance.Play("Click");
        currentCar = Instantiate(MainManager.instance.carHolder.Cars[carIndex].gameObject, showPos, showRot);
        currentCar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        if (currentCar.GetComponent<Car>().carDataHolder.isUnlocked)
        {

            lockedCarText.gameObject.SetActive(false);
         //   upgradeBtn.gameObject.SetActive(true);
            ShopBtn.gameObject.SetActive(false);
            MainManager.instance.currentCarIndex = carIndex;
        }
        else
        {
            lockedCarText.gameObject.SetActive(true);
            ShopBtn.gameObject.SetActive(true);

        //    upgradeBtn.gameObject.SetActive(false);
            lockCarIndex = carIndex;
        }
        Destroy(PreviousCar);
        carNameText.text = currentCar.GetComponent<Car>().carDataHolder.carName;
        carNameImage.sprite = currentCar.GetComponent<Car>().carDataHolder.sprite;
        CheckCarIsUnlocked();
        CheckCarIsUpgraded();
        //GameAnalytics.NewDesignEvent("Car_Selected");
    }


    public void OnExitButtonClick()
    {
        Application.Quit();
        AudioManager.instance.Play("Click");
    }

    public void GiveReward(int reward)
    {

        rewardedAdCanvas.SetActive(true);
        switch (reward)
        {
            case 0:
               // AdmobManager.instance.LoadRewardedAd(RewardType.Cash);

                break;
            case 1:
                //AdmobManager.instance.LoadRewardedAd(RewardType.Car);
                break;
        }

    }

    public void UnlockCar()
    {
        rewardedAdCanvas.SetActive(false);
        currentCar.GetComponent<Car>().carDataHolder.rewardedAdsCount += 1;
        if (currentCar.GetComponent<Car>().carDataHolder.rewardedAdsCount >= 3)
        {
            currentCar.GetComponent<Car>().carDataHolder.isUnlocked = true;
            OnCarSelection(lockCarIndex);
            PopUpManager.Instance.PopUp("You have unlocked: " + currentCar.GetComponent<Car>().carDataHolder.carName);
        }
        CheckCarIsUnlocked();
        DataSaver.instance.SaveData();
    }

    private void CheckCarIsUnlocked()
    {
        if (currentCar.GetComponent<Car>().carDataHolder.isUnlocked)
        {

            unlockBtn.SetActive(false);
        }
        else
        {
            unlockBtn.SetActive(true);
            rewardCountText.text = currentCar.GetComponent<Car>().carDataHolder.rewardedAdsCount.ToString() + "/3";

        }
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ig.car.sunt.ramp.game");
    }

    //Used in Garage Button OnClick Listener
    public void OpenGarage()
    {
        garageCam.SetActive(true);
        garageCanvas.SetActive(true);
        GarageCam.SetActive(false);
        mainMenuCanvas.SetActive(false);
        //GameAnalytics.NewDesignEvent("Garage_Open");
    }

    public void OnRewardGiven(RewardType reward)
    {
        rewardedAdCanvas.SetActive(false);
        //AdmobManager.instance.canShowAppOpen = true;
        switch (reward)
        {
            case RewardType.Cash:
                DataSaver.instance.cash += 2000;
                DataSaver.instance.SaveData();
                cashText.text = DataSaver.instance.cash.ToString();
                PopUpManager.Instance.PopUp("You have claim 2000 cash!");
                break;

            case RewardType.Car:
                UnlockCar();
                break;
        }
    }

    public void RewardFailed()
    {
        //AdmobManager.instance.canShowAppOpen = true;
        rewardedAdCanvas.SetActive(false);
        PopUpManager.Instance.PopUp("Cannot Show Ad due to Internet Connectivity Problem");
    }


    public void BuyCar()
    {

       
        if (/*DataSaver.instance.goldBars*/ 500 < currentCar.GetComponent<Car>().carDataHolder.carPrice) return;
        currentCar.GetComponent<Car>().carDataHolder.isUnlocked = true;
        OnCarSelection(lockCarIndex);
        PopUpManager.Instance.PopUp("You have unlocked: " + currentCar.GetComponent<Car>().carDataHolder.carName);

        DataSaver.instance.goldBars -= currentCar.GetComponent<Car>().carDataHolder.carPrice;
        DataSaver.instance.SaveData();

       
        goldBarsTxt.text = "GoldBars :" + DataSaver.instance.goldBars;
        
        CheckCarIsUnlocked();
    }
    public void CheckCarIsUpgraded()
    {
        if (currentCar.GetComponent<Car>().carDataHolder.isUpgraded)
        {
            MainManager.instance.carHolder.Cars[carIndex].carDataHolder.damage = false;
            MainManager.instance.carHolder.Cars[carIndex].carDataHolder.handeling = 60;
            MainManager.instance.carHolder.Cars[carIndex].carDataHolder.speed = 350;

        }
     //   else
      //      upgradeBtn.gameObject.SetActive(true);
        
          
        

        
    }


    public void CarUpgrade()
    {


        MainManager.instance.carHolder.Cars[carIndex].carDataHolder.damage = false;
        MainManager.instance.carHolder.Cars[carIndex].carDataHolder.handeling = 60;
        MainManager.instance.carHolder.Cars[carIndex].carDataHolder.speed = 350;
        CheckCarIsUpgraded();
        currentCar.GetComponent<Car>().carDataHolder.isUpgraded = true;
        DataSaver.instance.SaveData(); 
    

        /*
                WheelCollider[] wheelColliders = currentCar.GetComponentsInChildren<WheelCollider>();
                foreach (WheelCollider wheelCollider in wheelColliders)
                {
                    wheelCollider.mass += 20;
                }
                currentCar.GetComponent<Car>().GetComponent<RCC_CarControllerV3>().useDamage = false;
                currentCar.GetComponent<RCC_CarControllerV3>().useDamage = false;
                currentCar.GetComponent<RCC_CarControllerV3>().maxspeed = 300;

                currentCar.GetComponent<Car>().carDataHolder.isUpgraded = true;

                CheckCarIsUpgraded();
                DataSaver.instance.SaveData();*/
    }
        public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/13HQDkeGmoCPUASR8w26EPAKwe-HtOIgn0mClRetGvlU/edit");
    }


}

public enum RewardType
{
    Car,
    Cash
}