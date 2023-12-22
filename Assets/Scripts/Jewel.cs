using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour, IAttackable, ITargetable
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ToggleOutline toggleOutline;

    private JewelAudioManager audioManager;

    private void Start()
    {
        // Para obtener la referencia al JewelAudioManager en el mismo GameObject
        audioManager = GetComponent<JewelAudioManager>();
    }

    /// <summary>
    /// Set object targetted state
    /// </summary>
    /// <param name="value"></param>
    public void SetTargettedState(bool value)
    {
        toggleOutline.SetOutlines(value);
        
        // Llama al método en JewelAudioManager cuando cambia el estado objetivo
        audioManager.OnTargetStateChanged(value);
    }

    /// <summary>
    /// Runs when receiving attack by player
    /// </summary>
    public void ReceiveAttack()
    {
        if (deathParticles != null)
            deathParticles.Play();

        // Llama al método en JewelAudioManager cuando se recibe un ataque
        audioManager.ReceiveAttack();

        gameObject.SetActive(false);
    }
}
