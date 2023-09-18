using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    #region Singleton
    public static MainManager instance;

    private void Awake()
    {
        if (instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public int currentCarIndex;
    public CarHolder carHolder;
    public LevelHolder levelholder;
    public static int TotalLevels;
    public Sprite position1, position2, position3;
    private void Start()
    {
        TotalLevels = levelholder.Levels.Length; 
    }
    public bool ReturnNumberOfCarsBool()
    {
        if (currentCarIndex < carHolder.Cars.Length-1) return true;
        else
        {
            currentCarIndex = 0;
            return false;
        }
        
    }



    

}
