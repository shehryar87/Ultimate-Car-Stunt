using System;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{


    #region Singleton
    public static CameraChanger instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion



    public RCC_Camera carCamera;

    private void Start()
    {
       // Invoke(nameof(SetCamera), 2f);
        
    }

    public void SetCamera()
    {
        carCamera.TPSDistance = 6.25f;
        carCamera.TPSHeight = 1.8f;
    }

    public void OnJumpCamera()
    {
        carCamera.cameraMode = RCC_Camera.CameraMode.FIXED;
        Time.timeScale = 1f;
    }

    public void DefaultCamera()
    {
        carCamera.cameraMode = RCC_Camera.CameraMode.TPS;
        Time.timeScale = 1f;
    }
}
