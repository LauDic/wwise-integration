// Script created by Lautaro Dichio for the 3dar audio developer test.
// Define areas to send wwise state values for quick visualization and tracking within the scene.  

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AKLD_StateAreas : MonoBehaviour
{
    [System.Serializable]
    public class AreaData
    {
        // Configuración de la zona en forma de caja
        [Header("Box")]
        public Vector3 relativeCenter = Vector3.zero;
        public Vector3 size = new Vector3(1f, 1f, 1f);
        public string message = "Dentro del área";
        public Color gizmoColor = Color.yellow;

        // Configuración del estado
        [Header("State")]
        public AK.Wwise.State state = null;
        public bool changeStateOnExit = true;
        public AK.Wwise.State stateOut = null;

        [HideInInspector]
        public bool areaActivated = false;

        // Constructor por defecto para serialización
        public AreaData() { }

        // Método de inicialización
        public void Initialize()
        {
            size = new Vector3(1f, 1f, 1f);
        }
    }

    public List<AreaData> areas = new List<AreaData>() { new AreaData() };
    public Transform objetoAVerificar;

    private void Update()
    {
        if (objetoAVerificar != null)
        {
            foreach (var area in areas)
            {
                // Verifica si el objeto está dentro de la zona y la zona no está activada
                if (IsInsideArea(objetoAVerificar.position, area) && !area.areaActivated)
                {
                    Debug.Log(area.message);  // Muestra el mensaje de la zona

                    if (area.state != null) UpdateState(area.state);
                    area.areaActivated = true;  // Marca la zona como activada
                }
                // Si el objeto no está dentro de la zona
                else if (!IsInsideArea(objetoAVerificar.position, area))
                {
                    // Verifica si el objeto ha salido completamente de la zona
                    if (area.areaActivated)
                    {
                        area.areaActivated = false;  // Marca la zona como no activada

                        // Cambia el estado al salir de la zona si está configurado
                        if (area.changeStateOnExit && area.stateOut != null)
                        {
                            ChangeState(area.stateOut);
                        }
                    }
                }
            }
        }
    }

    private bool IsInsideArea(Vector3 position, AreaData area)
    {
        Vector3 areaCenter = transform.position + area.relativeCenter;
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

    private void UpdateState(AK.Wwise.State myState)
    {
        myState.SetValue();
    }

    private void ChangeState(AK.Wwise.State myState)
    {
        myState.SetValue();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AKLD_StateAreas))]
    public class AKLD_StateAreasEditor : Editor
    {
        // Dibuja gizmos en la escena del editor
        private void OnSceneGUI()
        {
            AKLD_StateAreas manager = target as AKLD_StateAreas;

            if (manager != null)
            {
                foreach (var area in manager.areas)
                {
                    DrawAreaGizmo(manager, area);
                }
            }
        }

        // Dibuja el inspector personalizado en el Editor de Unity
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Botón para inicializar los tamaños de las áreas
            if (GUILayout.Button("Initialize Sizes"))
            {
                InitializeSizes();
            }
        }

        // Inicializa los tamaños de las áreas
        private void InitializeSizes()
        {
            AKLD_StateAreas manager = target as AKLD_StateAreas;

            if (manager != null)
            {
                foreach (var area in manager.areas)
                {
                    area.Initialize();
                }
            }
        }

        // Dibuja el gizmo de la zona en la escena del editor
        private void DrawAreaGizmo(AKLD_StateAreas manager, AKLD_StateAreas.AreaData area)
        {
            Vector3 areaGlobalCenter = manager.transform.position + area.relativeCenter;

            Handles.color = area.gizmoColor;
            Handles.DrawWireCube(areaGlobalCenter, area.size);
        }
    }
#endif
}
