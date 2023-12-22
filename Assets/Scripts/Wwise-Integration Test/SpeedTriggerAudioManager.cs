using UnityEngine;

public class SpeedTriggerAudioManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event speedBoostEvent;

    // Llama a este método cuando se activa el evento de aumento de velocidad
    public void PlaySpeedBoostEvent()
    {
        // Reproduce el evento de aumento de velocidad de Wwise
        speedBoostEvent.Post(this.gameObject);
    }
}