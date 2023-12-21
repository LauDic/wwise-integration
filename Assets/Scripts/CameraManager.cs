using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraSettings cameraSettings;
    [SerializeField] private CinemachineFreeLook playerCamera;

    private void Awake()
    {
        SetCameraValues();
    }

    /// <summary>
    /// Set sensibility values for player camera
    /// </summary>
    private void SetCameraValues()
    {
        string controlScheme = PlayerPrefs.GetString(cameraSettings.playerPrefKey, " ");

        if (controlScheme == null)
            return;

        if (controlScheme == cameraSettings.playerPrefKeyKeyboard)
        {
            playerCamera.m_YAxis.m_MaxSpeed = cameraSettings.mouseYAxisMaxSpeed;
            playerCamera.m_XAxis.m_MaxSpeed = cameraSettings.mouseXAxisMaxSpeed;
        }
        else
        {
            playerCamera.m_YAxis.m_MaxSpeed = cameraSettings.controllerYAxisMaxSpeed;
            playerCamera.m_XAxis.m_MaxSpeed = cameraSettings.controllerXAxisMaxSpeed;
        }
    }
}
