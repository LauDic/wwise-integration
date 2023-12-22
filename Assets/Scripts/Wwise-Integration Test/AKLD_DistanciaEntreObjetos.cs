// Script created by Lautaro Dichio for the 3dar audio developer test.
// This script calculates the distance between two objects on a specified axis
// and sends the calculated value as an RTPC (Real-Time Parameter Control) to Wwise.

// Importing necessary libraries
using UnityEngine;

// Defining the class for calculating distance between objects
public class AKLD_DistanceBetweenObjects : MonoBehaviour
{
    // Variables to define the objects between which to calculate the distance
    [Header("Distance between Objects to RTPC")]
    public Transform object1;
    public Transform object2;

    // Variables to select the axis on which to calculate the distance
    [Space(10)]
    [Header("Choose Axis to Calculate Distance")]
    public AxisDistance axisDistance = AxisDistance.All;
    public enum AxisDistance
    {
        X, // only calculate distance on the X axis
        Y, // only calculate distance on the Y axis
        Z, // only calculate distance on the Z axis
        All // calculate distance on all axes
    }

    // Variable to store the calculated distance and view it in the inspector
    [Space(10)]
    [Header("Distance")]
    public float distance;

    // Variable for the RTPC that will be updated with the calculated distance
    [Header("RTPC")]
    [SerializeField]
    private AK.Wwise.RTPC rtpc = null;

    // Update is called once per frame
    void Update()
    {
        // Switch to determine on which axis to calculate the distance
        switch (axisDistance)
        {
            case AxisDistance.X:
                distance = Mathf.Abs(object1.position.x - object2.position.x);
                break;
            case AxisDistance.Y:
                distance = Mathf.Abs(object1.position.y - object2.position.y);
                break;
            case AxisDistance.Z:
                distance = Mathf.Abs(object1.position.z - object2.position.z);
                break;
            case AxisDistance.All:
                distance = Vector3.Distance(object1.position, object2.position);
                break;
        }

        // Update the RTPC with the value of the calculated distance
        rtpc.SetGlobalValue(distance);
    }
}

