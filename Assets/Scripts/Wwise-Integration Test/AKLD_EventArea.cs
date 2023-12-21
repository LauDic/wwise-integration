using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using AK.Wwise;

public class AKLD_EventArea : MonoBehaviour
{
    [System.Serializable]
    public class AreaData
    {
        // Configuración de la zona en forma de caja
        [Header("Box")]
        public Vector3 relativeCenter = Vector3.zero;  // Centro relativo de la zona con respecto al objeto principal
        public Vector3 size = new Vector3(1f, 1f, 1f);  // Tamaño de la zona en cada dimensión (x, y, z)
        public string message = "Dentro del área";  // Mensaje a mostrar cuando se activa la zona
        public Color gizmoColor = Color.yellow;  // Color del gizmo en el editor

        // Configuración del evento Wwise
        [Header("Event")]
        public AK.Wwise.Event myEvent = null;  // Evento de Wwise asociado a la zona
        public bool stopEventOnExit = true;  // Indica si se debe detener el evento al salir de la zona

        [HideInInspector]
        public bool areaActivated = false;  // Indica si la zona está activada actualmente

        // Constructor por defecto para serialización
        public AreaData() { }

        // Método de inicialización
        public void Initialize()
        {
            size = new Vector3(1f, 1f, 1f);  // Inicializa el tamaño por defecto
        }
    }

    // Lista de datos de áreas
    public List<AreaData> areas = new List<AreaData>() { new AreaData() };  // Al menos una área en la lista
    public Transform objetoAVerificar;  // Objeto cuya posición se verifica para determinar la activación de áreas

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
                    // Ejecuta el evento si está configurado
                    if (area.myEvent != null) UpdateEvent(area.myEvent);
                    area.areaActivated = true;  // Marca la zona como activada
                }
                // Si el objeto no está dentro de la zona
                else if (!IsInsideArea(objetoAVerificar.position, area))
                {
                    area.areaActivated = false;  // Marca la zona como no activada

                    // Detiene el evento al salir de la zona si está configurado
                    if (area.stopEventOnExit && area.myEvent != null)
                    {
                        StopEvent(area.myEvent);
                    }
                }
            }
        }
    }

    // Determina si una posición está dentro de una zona
    private bool IsInsideArea(Vector3 position, AreaData area)
    {
        Vector3 areaCenter = transform.position + area.relativeCenter;  // Centro global de la zona
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

     // Ejecuta un evento de Wwise
    private void UpdateEvent(AK.Wwise.Event myEvent)
    {
        myEvent.Post(this.gameObject);
    }

    // Detiene un evento de Wwise al salir del box
    private void StopEvent(AK.Wwise.Event myEvent)
    {
        myEvent.Stop(this.gameObject);
    }


#if UNITY_EDITOR
    // Editor personalizado para el inspector de Unity
    [CustomEditor(typeof(AKLD_EventArea))]
    public class AKLD_EventAreaEditor : Editor
    {
        // Dibuja gizmos en la escena del editor
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
            AKLD_EventArea manager = target as AKLD_EventArea;

            if (manager != null)
            {
                foreach (var area in manager.areas)
                {
                    area.Initialize();
                }
            }
        }

        // Dibuja el gizmo de la zona en la escena del editor
        private void DrawAreaGizmo(AKLD_EventArea manager, AKLD_EventArea.AreaData area)
        {
            Vector3 areaGlobalCenter = manager.transform.position + area.relativeCenter;

            Handles.color = area.gizmoColor;
            Handles.DrawWireCube(areaGlobalCenter, area.size);
        }
    }
#endif
}
