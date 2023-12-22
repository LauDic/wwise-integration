using UnityEngine;
using AK.Wwise;

public class AKLD_PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private AK.Wwise.Event movementSoundEvent;
    [SerializeField] private AK.Wwise.Event jumpSoundEvent;
    [SerializeField] private AK.Wwise.Event impactEvent;
    [SerializeField] private AK.Wwise.RTPC velocityImpact;
    public float velocityImpactf;

    private bool wasGrounded = false;
    private bool wasJumping = false;

    private void Start()
    {
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter not assigned. Please assign a PlayerCharacter to the script.");
        }

        movementSoundEvent.Post(gameObject);
        movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);

        jumpSoundEvent.Post(gameObject);
        jumpSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
    }

    private void Update()
    {
        if (playerCharacter != null)
        {
            bool isGrounded = playerCharacter.IsGrounded();

            if (isGrounded && !wasGrounded)
            {
                movementSoundEvent.Post(gameObject);
            }
            else if (!isGrounded && wasGrounded)
            {
                movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
            }

            bool isJumping = playerCharacter.IsJumping();

            if (isJumping && !wasJumping)
            {
                jumpSoundEvent.Post(gameObject);
            }

            wasGrounded = isGrounded;
            wasJumping = isJumping;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerCharacter != null && playerCharacter.IsGrounded())
        {
            // El jugador está en el suelo, no realizar el impacto
            return;
        }

        float impactSpeed = Mathf.Abs(Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal));
        velocityImpact.SetGlobalValue(impactSpeed);

        impactEvent.Post(this.gameObject);
        velocityImpactf = impactSpeed;
    }
}
