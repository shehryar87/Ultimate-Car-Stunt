using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
  
    BoxCollider obj;
    [SerializeField] private GameObject[] FireFlies;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            FireFlies[0].SetActive(true);
            FireFlies[1].SetActive(true);
            FireFlies[2].SetActive(true);
            Debug.Log("Finished");
            obj = GetComponent<BoxCollider>();
            obj.enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            other.GetComponentInParent<RCC_CarControllerV3>().enabled = false;
     
        //    other.GetComponentInParent<Rigidbody>().velocity = (0,0,0);
        //    LevelComplete(other.GetComponentInParent<Rigidbody>());
          var  Body = other.GetComponentInParent<Rigidbody>();
            StartCoroutine(LevelComplete(Body));
            
        }
    }

    IEnumerator LevelComplete(Rigidbody carRigidBody)
    {
        yield return new WaitForSeconds(1.5f);
        
        carRigidBody.velocity = Vector3.zero;
            
        GameplayManager.instance.LevelComplete();
        
     
    }
}
