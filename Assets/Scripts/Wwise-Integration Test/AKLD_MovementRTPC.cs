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

        // Inicializar el RTPC en un valor predeterminado si es necesario
        RTPCVelocity.SetValue(this.gameObject, 0.0f);
        forWindRTPCVelocity.SetValue(this.gameObject, 0.0f);
    }

    private void Update()
    {
        if (rb != null)
        {
            // Obtener la velocidad actual del Rigidbody
            float velocityMagnitude = rb.velocity.magnitude;

            // Actualizar el RTPC en Wwise con la velocidad del Rigidbody
            RTPCVelocity.SetGlobalValue(velocityMagnitude);
            RTPCVelocity.SetValue(this.gameObject,velocityMagnitude);
            velocidad = velocityMagnitude;
            forWindRTPCVelocity.SetValue(this.gameObject, velocityMagnitude);
        }
    }
}
