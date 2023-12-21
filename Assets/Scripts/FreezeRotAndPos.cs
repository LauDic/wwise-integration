using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotAndPos : MonoBehaviour
{
    [SerializeField] private bool freezeRotation;
    [SerializeField] private bool freezePosition;

    //TODO: Fix - Unclear logic - Why is this necessary?
    //Needs further discussion
    void Update()
    {
        if(freezeRotation)
            transform.rotation = Quaternion.Euler(0,0,0);

        if(freezePosition)
            transform.localPosition = Vector3.zero;
    }
}
