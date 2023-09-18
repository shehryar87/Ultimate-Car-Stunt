/*
using UnityEngine;

public class GAManager : MonoBehaviour
{
    public enum LevelStatus
    {
        Start,
        Complete,
        Failed,
    }


    #region Singleton
    public static GAManager instance;
    private void Awake()
    {
        if (instance != null)
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


    public void CustomDesignEvent(string designEvent)
    {
        //GameAnalytics.NewDesignEvent(designEvent);
    }

    public void LevelProgressionEvent(string levelEvent, LevelStatus levelStatus)
    {

        switch (levelStatus)
        {
            case LevelStatus.Start:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_" + levelEvent);
                break;

            case LevelStatus.Failed:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_" + levelEvent);
                break;

            case LevelStatus.Complete:
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + levelEvent);
                break;
        }

    }



}
*/