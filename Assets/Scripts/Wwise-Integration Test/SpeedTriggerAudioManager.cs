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