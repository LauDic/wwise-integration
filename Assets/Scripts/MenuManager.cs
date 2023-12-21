using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int firstLevelBuildIndex = 1;
    [SerializeField] private CameraSettings cameraSettings;

    [SerializeField] private TextMeshProUGUI controlText;

    private bool isKeyboard = true;

    private void Awake()
    {
        if (isKeyboard)
        {
            PlayerPrefs.SetString(cameraSettings.playerPrefKey, cameraSettings.playerPrefKeyKeyboard);

            controlText.text = "Keyboard";
        }
        else
        {
            PlayerPrefs.SetString(cameraSettings.playerPrefKey, cameraSettings.playerPrefKeyController);

            controlText.text = "Controller";
        }
    }

    /// <summary>
    /// Loads first game level
    /// </summary>
    public void LoadFirstLevel()
    {
        GameManager.Instance.LoadLevel(firstLevelBuildIndex);
    }

    /// <summary>
    /// Toggles game settings from keyboard to controller
    /// </summary>
    public void ToggleControlScheme()
    {
        isKeyboard = !isKeyboard;

        if (isKeyboard)
        {
            PlayerPrefs.SetString(cameraSettings.playerPrefKey, cameraSettings.playerPrefKeyKeyboard);

            controlText.text = "Keyboard";
        }
        else
        {
            PlayerPrefs.SetString(cameraSettings.playerPrefKey, cameraSettings.playerPrefKeyController);

            controlText.text = "Controller";
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
