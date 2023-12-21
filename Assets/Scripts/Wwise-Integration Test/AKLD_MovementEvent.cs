using UnityEngine;
using AK.Wwise;

public class SoundController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private AK.Wwise.Event soundEvent; // Objeto del evento de sonido en Wwise

    private bool wasGrounded = false; // Variable para rastrear el estado anterior de estar en el suelo

    private void Start()
    {
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter not assigned. Please assign a PlayerCharacter to the script.");
        }

        // Inicializar el evento de sonido en un estado pausado
        soundEvent.Post(gameObject);
        soundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
    }

    private void Update()
    {
        if (playerCharacter != null)
        {
            // Verificar cambio de estado de estar en el suelo
            bool isGrounded = playerCharacter.IsGrounded();

            // Reproducir o pausar el evento de sonido según el cambio de estado del jugador
            if (isGrounded && !wasGrounded)
            {
                soundEvent.Post(gameObject);
            }
            else if (!isGrounded && wasGrounded)
            {
                soundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
            }

            // Actualizar el estado anterior
            wasGrounded = isGrounded;
        }
    }
}
