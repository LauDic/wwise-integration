// Script created by Lautaro Dichio for the 3dar audio developer test.
// This script is in charge of controlling all wwise events related to the movement of the ball. 
using UnityEngine;

public class AKLD_PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private AK.Wwise.Event movementSoundEvent;
    [SerializeField] private AK.Wwise.Event jumpSoundEvent;
    [SerializeField] private AK.Wwise.Event impactEvent;
    [SerializeField] private AK.Wwise.Event dashSoundEvent;
    [SerializeField] private AK.Wwise.RTPC velocityImpact;
    public float velocityImpactf;

    private bool wasGrounded = false;
    private bool wasJumping = false;
    private bool wasAttacking = false;
    private float groundedCheckDelay = 1.0f;
    private float timeSinceLastGrounded = 0.0f;


    private void Start()
    {
        
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter not assigned. Please assign a PlayerCharacter to the script.");
        }

        // Se inicializan eventos de movimiento en wwise.
        movementSoundEvent.Post(gameObject);
        movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);

        jumpSoundEvent.Post(gameObject);
        jumpSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
    }

    private void Update()
    {
        if (playerCharacter == null)
        {
            return;
        }

        bool isGrounded = playerCharacter.IsGrounded();
        bool isJumping = playerCharacter.IsJumping();
        bool isAttacking = playerCharacter.IsAttacking();

        
        ManageMovementSound(isGrounded);
        PlayJumpSound(isJumping);

        if (!isGrounded)
        {
            timeSinceLastGrounded += Time.deltaTime;
        }

        if (isAttacking && !wasAttacking)
        {
            dashSoundEvent.Post(gameObject);
        }

        wasGrounded = isGrounded;
        wasJumping = isJumping;
        wasAttacking = isAttacking;
    }

    
    private void ManageMovementSound(bool isGrounded)
    {
        //  Playing movement sound based on character status.
        if (isGrounded && !wasGrounded)
        {
            movementSoundEvent.Post(gameObject);
        }
        else if (!isGrounded && wasGrounded)
        {
            movementSoundEvent.Stop(gameObject, 0, AkCurveInterpolation.AkCurveInterpolation_Linear);
        }
    }

    private void PlayJumpSound(bool isJumping)
    {
        // Playing jumping sound if the character is jumping. 
        if (isJumping && !wasJumping)
        {
            jumpSoundEvent.Post(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if enough time has passed since the last jump to avoid impact sounds when rolling between game objects.    
        if (timeSinceLastGrounded >= groundedCheckDelay)
        {
            //Calculates the impact velocity based on the collision information.
            float impactSpeed = Mathf.Abs(Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal));

            //Set the value of the impact velocity variable in an rtpc to determine the impact volume. 
            velocityImpact.SetGlobalValue(impactSpeed);

            //Impact sound reproduction.  
            impactEvent.Post(this.gameObject);
            velocityImpactf = impactSpeed;

            //Reset the time since the last jump.
            timeSinceLastGrounded = 0.0f;
        }
    }
}
