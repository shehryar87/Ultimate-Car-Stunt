using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStats : MonoBehaviour
{
    public int levelNumber;
    public Image position1;
    public Image position2;
    public Image position3;
    public GameObject lockLevel;
    public TextMeshProUGUI cashText;
    void Start()
    {

        cashText.text = MainManager.instance.levelholder.Levels[levelNumber-1].cashReward.ToString();   
        if (DataSaver.instance.carData.isLevelUnlocked[levelNumber - 1])
        {
            lockLevel.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }

        switch (DataSaver.instance.carData.positions[levelNumber - 1])
        {

            case 1:
               // position3.sprite = MainMenuUI.instance.position3;
                break;
            case 2:
               // position2.sprite = MainMenuUI.instance.position2;
                //position3.sprite = MainMenuUI.instance.position3;
                break;
            case 3:
                //position1.sprite = MainMenuUI.instance.position1;
                //position2.sprite = MainMenuUI.instance.position2;
                //position3.sprite = MainMenuUI.instance.position3;
                break;

        }
    }


}
