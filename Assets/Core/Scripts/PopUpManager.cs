using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{

    public static PopUpManager Instance;
    [SerializeField] private GameObject popUp;
    public GameObject yesBtn, noBtn, okBtn;
    public TextMeshProUGUI notification;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
    }

   
    public void PopUp(string notification)
    {
        GetComponent<Canvas>().enabled = true;
        popUp.transform.DOScale(1f, 1f).SetEase(Ease.OutBack);
        this.notification.text = notification;
    }

    public void NotiClick()
    {
        popUp.transform.DOScale(0f, 0.6f).SetEase(Ease.InBack).OnComplete(() =>
        {
            GetComponent<Canvas>().enabled = false;
        });
    }
}
