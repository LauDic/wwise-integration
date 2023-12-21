using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOutline : MonoBehaviour
{
    [SerializeField] private Outline[] outlines;

    public void SetOutlines(bool value)
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = value;
        }
    }
}
