using UnityEngine;

public class AKLD_RespawnAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event wwiseEvent;
    [SerializeField] private AK.Wwise.Switch wwiseSwitch;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Puedes ajustar la condici�n seg�n tus necesidades
        {
            // Cambia el switch
            if (wwiseSwitch != null)
            {
                wwiseSwitch.SetValue(gameObject);
            }

            // Env�a el evento
            if (wwiseEvent != null)
            {
                wwiseEvent.Post(gameObject);
            }
        }
    }
}