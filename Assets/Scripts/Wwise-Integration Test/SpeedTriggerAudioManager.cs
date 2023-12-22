// Script created by Lautaro Dichio for the 3dar audio developer test.
// Playback of the speed increase sound. 

using UnityEngine;

public class SpeedTriggerAudioManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event speedBoostEvent;

    // Call this method when the speed increase event is triggered.
    public void PlaySpeedBoostEvent()
    {
        // Plays Wwise speed-up event
        speedBoostEvent.Post(this.gameObject);
    }
}