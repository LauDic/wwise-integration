using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour, IAttackable, ITargetable
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ToggleOutline toggleOutline;

    /// <summary>
    /// Set object targetted state
    /// </summary>
    /// <param name="value"></param>
    public void SetTargettedState(bool value)
    {
        toggleOutline.SetOutlines(value);
    }

    /// <summary>
    /// Runs when receiving attack by player
    /// </summary>
    public void ReceiveAttack()
    {
        if (deathParticles != null)
            deathParticles.Play();

        gameObject.SetActive(false);
    }
}
