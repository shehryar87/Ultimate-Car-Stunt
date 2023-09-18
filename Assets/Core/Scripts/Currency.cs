using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoldBar"))
        {
            TimeTrialTimer.Instance.AddGold(10);
            other.gameObject.SetActive(false);
            Debug.Log("GOLDBAR", gameObject);
        }

       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
            AudioManager.instance.Play("Ball");
    }
}
