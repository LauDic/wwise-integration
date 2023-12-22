//Script for the sound of the impact of the ball in the water, due to time issues it was not solved.

using UnityEngine;

public class AKLD_SplashWater : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event wwiseEvent;
    [SerializeField] private AK.Wwise.Switch wwiseSwitch;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            // Cambia el switch
            if (wwiseSwitch != null)
            {
                wwiseSwitch.SetValue(this.gameObject);
            }

            // Envía el evento
            if (wwiseEvent != null)
            {
                wwiseEvent.Post(gameObject);
            }
        }
    }
}