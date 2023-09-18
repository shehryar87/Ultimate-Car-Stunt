using System.IO;
using UnityEngine;


public class DataSaver : MonoBehaviour
{

    #region Singleton
    public static DataSaver instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private string saveFilePath;
    [SerializeField] MainManager mainManager;
    public CarData carData = new CarData { };
    public int cash;
    public int goldBars;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + " v2GameData.json";
        if (PlayerPrefs.HasKey("FirstSave") && PlayerPrefs.GetInt("FirstSave") == 1)
        {
            LoadData();

        }
        else
        {
            PlayerPrefs.SetInt("FirstSave", 1);
            SaveData();
        }
    }

    public void SaveData()
    {
        for (int i = 0; i < mainManager.carHolder.Cars.Length; i++)
        {
           
            carData.carPrice[i] = mainManager.carHolder.Cars[i].carDataHolder.carPrice;
            carData.isUnlocked[i] = mainManager.carHolder.Cars[i].carDataHolder.isUnlocked;
            carData.rewardedAdsCount[i] = mainManager.carHolder.Cars[i].carDataHolder.rewardedAdsCount;
            carData.damage[i] = mainManager.carHolder.Cars[i].carDataHolder.damage;
            carData.handeling[i] = mainManager.carHolder.Cars[i].carDataHolder.handeling;
            carData.speed[i] = mainManager.carHolder.Cars[i].carDataHolder.speed;
        }
        for (int i = 0; i < mainManager.levelholder.Levels.Length; i++)
        {
            carData.isLevelUnlocked[i] = mainManager.levelholder.Levels[i].isLevelUnlocked;
            carData.positions[i] = mainManager.levelholder.Levels[i].position;
        }
        carData.cash = cash;
        carData.goldBars = goldBars;
        string saveDataCars = JsonUtility.ToJson(carData);

        File.WriteAllText(saveFilePath, saveDataCars);
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string loadedDataCars = File.ReadAllText(saveFilePath);
            carData = JsonUtility.FromJson<CarData>(loadedDataCars);


            for (int i = 0; i < mainManager.carHolder.Cars.Length; i++)
            {
                
                mainManager.carHolder.Cars[i].carDataHolder.carPrice = carData.carPrice[i];
                mainManager.carHolder.Cars[i].carDataHolder.isUnlocked = carData.isUnlocked[i];
                mainManager.carHolder.Cars[i].carDataHolder.rewardedAdsCount = carData.rewardedAdsCount[i];
                
            }
            for (int i = 0; i < mainManager.levelholder.Levels.Length; i++)
            {
                mainManager.levelholder.Levels[i].isLevelUnlocked = carData.isLevelUnlocked[i];
                mainManager.levelholder.Levels[i].position = carData.positions[i];
            }
            cash = carData.cash;
            goldBars = carData.goldBars;
        }

        else
        {
            Debug.Log("No Data to Load");
        }
    }




}

[System.Serializable]
public class CarData
{
    //===Car Data===//
    public int[] carPrice;
    public bool[] isUnlocked;
    public int[] rewardedAdsCount;
    //===Cash and Gold Bars===//
    public int cash;
    public int goldBars;
    //===CarUpgrades===//
    public bool[] damage;
    public int[] handeling;
    public int[] speed;
    //===Level Data===//
    [Header("Levels Data")]
    public int[] levelNumber;
    public bool[] isLevelUnlocked;
    public int[] positions;
}
