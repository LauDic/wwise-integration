// Script created by Lautaro Dichio for the 3dar audio developer test.
// calculates player speed based on rigidbody and creates rtpc to send to wwise

using UnityEngine;

public class AKLD_MovementRTPC : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AK.Wwise.RTPC RTPCVelocity; // Nombre del RTPC en Wwise
    [SerializeField] private AK.Wwise.RTPC forWindRTPCVelocity; // Nombre del RTPC en Wwise
    public float velocidad;


    private void Start()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody not assigned. Please assign a Rigidbody to the script.");
        }

        // Initialize the RTPC to a default value if required

        RTPCVelocity.SetValue(this.gameObject, 0.0f);
        forWindRTPCVelocity.SetValue(this.gameObject, 0.0f);
    }

    private void Update()
    {
        if (rb != null)
        {
            // Get the current Rigidbody speed
            float velocityMagnitude = rb.velocity.magnitude;

            // Updating the RTPC in Wwise with Rigidbody speed
            RTPCVelocity.SetGlobalValue(velocityMagnitude);
            RTPCVelocity.SetValue(this.gameObject,velocityMagnitude);
            velocidad = velocityMagnitude;
            forWindRTPCVelocity.SetValue(this.gameObject, velocityMagnitude);
        }
    }
}
