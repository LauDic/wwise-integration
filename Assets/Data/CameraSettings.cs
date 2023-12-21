using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettingsData", menuName = "ScriptableObjects/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    public string playerPrefKey = "ControlScheme";
    public string playerPrefKeyKeyboard = "Keyboard";
    public string playerPrefKeyController = "Controller";

    [Header("Controller")]
    public float controllerYAxisMaxSpeed = 0.1f;
    public float controllerXAxisMaxSpeed = 3.0f;

    [Header("Mouse and Keyboard")]
    public float mouseYAxisMaxSpeed = 0.01f;
    public float mouseXAxisMaxSpeed = 0.2f;
}
