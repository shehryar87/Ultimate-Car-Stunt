using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{

    public Transform RCC_Camera;
    // Update is called once per frame
    private void Start()
    {
        transform.parent = GameplayManager.instance.car.transform;
    }
    void Update()
    {
        
        transform.LookAt(RCC_Camera);
    }
}
