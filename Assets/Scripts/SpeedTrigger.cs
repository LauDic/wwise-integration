using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTrigger : MonoBehaviour
{
    [SerializeField] private PlayerTriggerDetector playerTrigger;
    [SerializeField] private float speedBoost;
    [SerializeField] private SpeedTriggerAudioManager audioManager; // Referencia al SpeedTriggerAudioManager

    private void Awake()
    {
        playerTrigger.OnPlayerTrigger += GivePlayerBoost;
    }

    // Call this method when the speed increase event is triggered.
    private void PlaySpeedBoostEvent()
    {
        // Checks if the audio manager is assigned before calling the method
        if (audioManager != null)
        {
            audioManager.PlaySpeedBoostEvent();
        }
    }


    //TODO: Fix - It would be better to just have an OnTriggerEnter in this component, the event will be triggered anyways
    //I prefer using my player trigger class, since it automates the verification process - this can be discussed
    /// <summary>
    /// Adds speed boost to player when entering its trigger
    /// </summary>
    /// <param name="controller"></param>
    private void GivePlayerBoost(PlayerController controller)
    {
        controller.PlayerCharacter.AddSpeed(transform.forward,speedBoost);


        // Calls the method in SpeedTriggerAudioManager when the speedup event is triggered
        PlaySpeedBoostEvent();
    }
}
