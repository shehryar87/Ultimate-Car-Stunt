using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrialTimer : MonoBehaviour
{

    public static TimeTrialTimer Instance;
    [SerializeField] private TextMeshProUGUI[] timerText;
    [SerializeField] private TextMeshProUGUI bronzeText, silverText, goldText;

    public static int goldBars = 0;
    private void Awake()
    {
        Instance = this;
    }
    private float startTime;
    private bool isRunning = false;
    private float totalTime = 60f; // Total time in seconds

    private void Start()
    {

        goldBars = 0;
        if (MainMenuUI.timeMode)
        {
            timerText[0].gameObject.SetActive(true);
            timerText[1].gameObject.SetActive(true);
        }
        
    }

    private void Update()
    {
        if (isRunning)
        {
            // Calculate the remaining time
            float remainingTime = totalTime - (Time.time - startTime);

            // Check if time has run out
            if (remainingTime <= 0)
            {
                // Time's up!
                StopTimer();
                remainingTime = 0;
                GameplayManager.instance.LevelFailed();
            }

            // Format the remaining time as minutes and seconds
            string minutes = ((int)remainingTime / 60).ToString("00");
            string seconds = (remainingTime % 60).ToString("00");

            // Update the timer text
            timerText[0].text = minutes + ":" + seconds;
            timerText[1].text = minutes + ":" + seconds;
        }
    }

    public void StartTimer(float timer)
    {
        timerText[0].gameObject.SetActive(true);
        timerText[1].gameObject.SetActive(true);

        // Start the timer
        totalTime = timer;
        startTime = Time.time;
        isRunning = true;
       // bronzeText.text = (totalTime * 0.1f).ToString();

     //   TimeSetUI(totalTime * 0.1f, bronzeText);
     //   TimeSetUI(totalTime * 0.5f, silverText);
     //   TimeSetUI(totalTime * 0.7f, goldText);


        //silverText.text = (totalTime * 0.5f).ToString();
        //goldText.text = (totalTime * 0.8f).ToString();
    }

    public void StopTimer()
    {
        // Stop the timer
        isRunning = false;
        
    }
   
    public float GetRemainingTime()
    {
        // Get the remaining time
        return totalTime - (Time.time - startTime);
    }

    public void TimeSetUI(float time, TextMeshProUGUI text)
    {
        string minutes = ((int)time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        text.text = minutes + ":" + seconds;
    }

    public void AddGold(int gold)
    {
        goldBars += gold;
        goldText.text = goldBars.ToString();
       
    }
}