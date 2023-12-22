using UnityEngine;

public class JewelAudioManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event attackReceivedEvent;
    [SerializeField] private AK.Wwise.Event onTarget;
    [SerializeField] private AK.Wwise.Event offTarget;

    private bool isTargeted = false;

    // Llama a este m�todo cuando se recibe un ataque
    public void ReceiveAttack()
    {
        // Reproduce el sonido de part�culas o realiza otras acciones de audio aqu�
        attackReceivedEvent.Post(this.gameObject);
    }

    // Llama a este m�todo cuando cambia el estado objetivo
    public void OnTargetStateChanged(bool isTargeted)
    {
        // Si cambia a true y no estaba previamente en true, env�a el evento onTarget
        if (isTargeted && !this.isTargeted)
        {
            onTarget.Post(this.gameObject);
        }
        // Si cambia a false y estaba previamente en true, env�a el evento offTarget
        else if (!isTargeted && this.isTargeted)
        {
            offTarget.Post(this.gameObject);
        }

        this.isTargeted = isTargeted;
    }
}
