using UnityEngine;

public class JewelAudioManager : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event attackReceivedEvent;
    [SerializeField] private AK.Wwise.Event onTarget;
    [SerializeField] private AK.Wwise.Event offTarget;

    private bool isTargeted = false;

    // call this method when an attack is received.
    public void ReceiveAttack()
    {
        // Play the particle sound or perform other audio actions here
        attackReceivedEvent.Post(this.gameObject);
    }

    //  Call this method when the target state changes.
    public void OnTargetStateChanged(bool isTargeted)
    {
        // If it changes to true and was not previously set to true, it sends the onTarget event
        if (isTargeted && !this.isTargeted)
        {
            onTarget.Post(this.gameObject);
        }
        // If it changes to false and it was previously set to true, it sends the offTarget event.
        else if (!isTargeted && this.isTargeted)
        {
            offTarget.Post(this.gameObject);
        }

        this.isTargeted = isTargeted;
    }
}
