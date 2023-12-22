using UnityEngine;
using AK.Wwise;

public class AKLD_PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private AK.Wwise.Event movementSoundEvent; // Evento de sonido para el movimiento
    [SerializeField] private AK.Wwise.Event jumpSoundEvent; // Evento de sonido para el salto
    [SerializeField] private AK.Wwise.Event impactEvent; // Nombre del evento de impacto en Wwise
    [SerializeField] private AK.Wwise.RTPC velocityImpact; // Nombre del RTPC de velocidad en Wwise
    public float velocityImpactf;

    private bool wasGrounded = false; // Variable para rastrear el estado anterior de estar en el suelo
    private bool wasJumping = false; // Variable para rastrear el estado anterior de estar saltando



    private void Start()
    {
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter not assigned. Please assign a PlayerCharacter to the script.");
        }

        // Inicializar el evento de sonido de movimiento en un estado pausado
        movementSoundEvent.Post(gameObject);
        movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);

        // Inicializar el evento de sonido de salto en un estado pausado
        jumpSoundEvent.Post(gameObject);
        jumpSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
    }

    private void Update()
    {
        if (playerCharacter != null)
        {
            // Verificar cambio de estado de estar en el suelo
            bool isGrounded = playerCharacter.IsGrounded();

            // Reproducir o pausar el evento de sonido de movimiento según el cambio de estado del jugador
            if (isGrounded && !wasGrounded)
            {
                movementSoundEvent.Post(gameObject);
            }
            else if (!isGrounded && wasGrounded)
            {
                movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
            }

            // Verificar cambio de estado de estar saltando
            bool isJumping = playerCharacter.IsJumping();

            // Reproducir el evento de sonido de salto solo cuando el jugador comienza a saltar
            if (isJumping && !wasJumping)
            {
                jumpSoundEvent.Post(gameObject);
            }

            // Actualizar el estado anterior
            wasGrounded = isGrounded;
            wasJumping = isJumping;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Obtener la velocidad relativa en el eje de impacto
        float impactSpeed = Mathf.Abs(Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal));

        // Ajustar el RTPC de velocidad según la velocidad relativa
        velocityImpact.SetGlobalValue(impactSpeed);


        // Reproducir el evento de impacto
        impactEvent.Post(this.gameObject);
        velocityImpactf = impactSpeed;
    }

}
