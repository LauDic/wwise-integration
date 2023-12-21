using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerDetector : MonoBehaviour
{
    public Action<PlayerController> OnPlayerTrigger;
    public Action<PlayerController> OnPlayerTriggerExit;

    [SerializeField] private UnityEvent<PlayerController> OnPlayerTriggerEvent;


    private void Awake()
    {
        OnPlayerTrigger += (PlayerController controller) => { OnPlayerTriggerEvent?.Invoke(controller); };
    }

    private void OnDestroy()
    {
        OnPlayerTrigger -= (PlayerController controller) => { OnPlayerTriggerEvent?.Invoke(controller); };
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnPlayerTrigger?.Invoke(playerController);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            OnPlayerTriggerExit?.Invoke(other.GetComponent<PlayerController>());
        }
    }
}
