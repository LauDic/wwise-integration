// Script created by Lautaro Dichio for the 3dar audio developer test.
// Creates Areas that trigger wwise events

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AK.Wwise;

public class AKLD_EventArea : MonoBehaviour
{
    [System.Serializable]
    public class AreaData
    {
        // Zone configuration in the form of a box
        [Header("Box")]
        public Vector3 relativeCenter = Vector3.zero;  // Relative center of the zone with respect to the main object
        public Vector3 size = new Vector3(1f, 1f, 1f);  // Size of the zone in each dimension (x, y, z)
        public string message = "Inside the area";  // Message to display when the zone is activated
        public Color gizmoColor = Color.yellow;  // Gizmo color in the editor

        // Wwise event configuration
        [Header("Event")]
        public AK.Wwise.Event myEvent = null;  // Wwise event associated with the zone
        public bool stopEventOnExit = true;  // Indicates whether to stop the event when exiting the zone

        [HideInInspector]
        public bool areaActivated = false;  // Indicates if the zone is currently activated

        // Default constructor for serialization
        public AreaData() { }

        // Initialization method
        public void Initialize()
        {
            size = new Vector3(1f, 1f, 1f);  // Initialize the default size
        }
    }

    // List of area data
    public List<AreaData> areas = new List<AreaData>() { new AreaData() };  // At least one area in the list
    public Transform objectToCheck;  // Object whose position is checked to determine area activation

    private void Update()
    {
        if (objectToCheck != null)
        {
            foreach (var area in areas)
            {
                // Check if the object is inside the zone and the zone is not activated
                if (IsInsideArea(objectToCheck.position, area) && !area.areaActivated)
                {
                    Debug.Log(area.message);  // Display the zone message
                    // Trigger the event if configured
                    if (area.myEvent != null) UpdateEvent(area.myEvent);
                    area.areaActivated = true;  // Mark the zone as activated
                }
                // If the object is not inside the zone
                else if (!IsInsideArea(objectToCheck.position, area))
                {
                    area.areaActivated = false;  // Mark the zone as not activated

                    // Stop the event when exiting the zone if configured
                    if (area.stopEventOnExit && area.myEvent != null)
                    {
                        StopEvent(area.myEvent);
                    }
                }
            }
        }
    }

    // Determines if a position is inside a zone
    private bool IsInsideArea(Vector3 position, AreaData area)
    {
        Vector3 areaCenter = transform.position + area.relativeCenter;  // Global center of the zone
        float minX = areaCenter.x - area.size.x / 2;
        float maxX = areaCenter.x + area.size.x / 2;
        float minY = areaCenter.y - area.size.y / 2;
        float maxY = areaCenter.y + area.size.y / 2;
        float minZ = areaCenter.z - area.size.z / 2;
        float maxZ = areaCenter.z + area.size.z / 2;

        return (position.x > minX && position.x < maxX &&
                position.y > minY && position.y < maxY &&
                position.z > minZ && position.z < maxZ);
    }

    // Triggers a Wwise event
    private void UpdateEvent(AK.Wwise.Event myEvent)
    {
        myEvent.Post(this.gameObject);
    }

    // Stops a Wwise event when exiting the box
    private void StopEvent(AK.Wwise.Event myEvent)
    {
        myEvent.Stop(this.gameObject);
    }


#if UNITY_EDITOR
    // Custom editor for the Unity inspector
    [CustomEditor(typeof(AKLD_EventArea))]
    public class AKLD_EventAreaEditor : Editor
    {
        // Draws gizmos in the editor scene
        private void OnSceneGUI()
        {
            AKLD_EventArea manager = target as AKLD_EventArea;

            if (manager != null)
            {
                foreach (var area in manager.areas)
                {
                    DrawAreaGizmo(manager, area);
                }
            }
        }

        // Draws the custom inspector in the Unity Editor
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Button to initialize area sizes
            if (GUILayout.Button("Initialize Sizes"))
            {
                InitializeSizes();
            }
        }

        // Initializes area sizes
        private void InitializeSizes()
        {
            AKLD_EventArea manager = target as AKLD_EventArea;

            if (manager != null)
            {
                foreach (var area in manager.areas)
                {
                    area.Initialize();
                }
            }
        }

        // Draws the gizmo of the zone in the editor scene
        private void DrawAreaGizmo(AKLD_EventArea manager, AKLD_EventArea.AreaData area)
        {
            Vector3 areaGlobalCenter = manager.transform.position + area.relativeCenter;

            Handles.color = area.gizmoColor;
            Handles.DrawWireCube(areaGlobalCenter, area.size);
        }
    }
#endif
}





