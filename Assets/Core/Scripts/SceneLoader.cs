using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    [SerializeField] private InHouseAds inHouse;
    #region Singleton
    public static SceneLoader instance;
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
    public enum Scenes
    {
        Splash,
        MainMenu,
        Gameplay,
        LevelSelection
    }

    public GameObject loading;
    public Scenes currentScene;
    public Scenes lastScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += TurnLoadingOn;
    }

    private void TurnLoadingOn(Scene arg0, LoadSceneMode arg1)
    {
        loading.SetActive(false);   
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= TurnLoadingOn;
    }

    public void GoToGameplay()
    {
        currentScene = Scenes.Gameplay;
        SceneManager.LoadScene(currentScene.GetHashCode());
    }
    public void LoadNextScene(Scenes scene)
    {
        if (currentScene == Scenes.Splash)
        {
            loading.SetActive(false);
           // AdmobManager.instance.HideBanner(AdmobManager.BannerAD.Both);
        }

        else
        {
            loading.SetActive(true);
            //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.LargeBanner);
            //AdmobManager.instance.HideBanner(AdmobManager.BannerAD.SmallBanner);
            //inHouse.Show(true);
        }
        lastScene = currentScene;
        currentScene = scene;

        StartCoroutine(Loading(scene));
        
    }

    //Used in Animation Event in LoadingText Script
    public void LoadingScreenAnimation()
    {
       // StartCoroutine(Loading(currentScene));

        
    }

    IEnumerator Loading(Scenes scene)
    {
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene.GetHashCode());
        //loading.SetActive(false);
       
    }
   
}
