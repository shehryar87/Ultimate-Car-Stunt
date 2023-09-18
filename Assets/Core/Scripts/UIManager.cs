using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion


    //=====Buttons=====//
    [SerializeField] private Button nextLevelButton, RestartButton, homeButton,pauseButton;
    [SerializeField] public GameObject levelFailedCanvas, levelPauseCanvas,rccCanvas,loadingScreen;
    [SerializeField] public TextMeshProUGUI cashReward , goldBarReward;
    [SerializeField] private TextMeshProUGUI yourTimeFailedText, bronzeTimeFailedText, silverTimeFailedText, goldTimeFailedText;
    [SerializeField] private TextMeshProUGUI yourTimeCompleteText, bronzeTimeCompleteText, silverTimeCompleteText, goldTimeCompleteText,rewindText;
    //=====Done=====//

    public GameObject levelCompletePanel;
    public Image position1, position2, position3;
    private void Start()
    {
        //nextLevelButton.onClick.AddListener(()=>GameplayManager.instance.NextLevel());
        //RestartButton.onClick.AddListener(() => GameplayManager.instance.RestartLevel());
        //pauseButton.onClick.AddListener(() => GameplayManager.instance.PauseGame());
       // homeButton.onClick.AddListener(() =>SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu));
        
        
    }


    public void OnLevelFailed()
    {
        levelFailedCanvas.SetActive(true);
        rccCanvas.SetActive(false);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer*0.7f,goldTimeFailedText);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer*0.5f,silverTimeFailedText);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer*0.1f,bronzeTimeFailedText);
        //TimeTrialTimer.Instance.TimeSetUI(TimeTrialTimer.Instance.GetRemainingTime() * 0.1f, yourTimeFailedText);
        //string minutes = ((int)TimeTrialTimer.Instance.GetRemainingTime() / 60).ToString("00");
        //string seconds = (TimeTrialTimer.Instance.GetRemainingTime() % 60).ToString("00");
        //yourTimeFailedText.text = minutes + ":" + seconds; 
    }
    

    public void OnLevelComplete(int levelIndex)
    {
        goldBarReward.text = TimeTrialTimer.goldBars.ToString();
        levelCompletePanel.SetActive(true);
        rccCanvas.SetActive(false);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer * 0.7f, goldTimeCompleteText);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer * 0.5f, silverTimeCompleteText);
        //TimeTrialTimer.Instance.TimeSetUI(GameplayManager.instance.levelTimer * 0.1f, bronzeTimeCompleteText);
        //TimeTrialTimer.Instance.TimeSetUI(TimeTrialTimer.Instance.GetRemainingTime() * 0.1f, yourTimeCompleteText);
        //string minutes = ((int)(TimeTrialTimer.Instance.GetRemainingTime()+2) / 60).ToString("00");
        //string seconds = ((TimeTrialTimer.Instance.GetRemainingTime()+2) % 60).ToString("00");
//yourTimeCompleteText.text = minutes + ":" + seconds;

       /* switch (MainManager.instance.levelholder.Levels[levelIndex].position)
        {

            case 1:
                position1.sprite = MainManager.instance.position1;
                break;
            case 2:
                position2.sprite = MainManager.instance.position2;
                position1.sprite = MainManager.instance.position1;
                break;
            case 3:
                position1.sprite = MainManager.instance.position1;
                position2.sprite = MainManager.instance.position2;
                position3.sprite = MainManager.instance.position3;
                break;

        }*/
    }
    public void OnRewind()
    {
        rewindText.text = "x " + GameplayManager.RewindCount;
    }
}
