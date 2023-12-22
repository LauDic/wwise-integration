// Script creado por Lautaro Dichio
// Este script calcula la distancia entre dos objetos en un determinado eje
// y envía el valor calculado como un RTPC (Real-Time Parameter Control) a Wwise.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AKLD_DistanciaEntreObjetos;

public class AKLD_DistanciaEntreObjetos : MonoBehaviour
{
    // Variables para definir los objetos entre los cuales calcular la distancia
    [Header("Distancia entre GO a RTPC")]
    public Transform objeto1;
    public Transform objeto2;

    // Variables para seleccionar el eje en el que calcular la distancia
    [Space(10)]
    [Header("Elegir Eje en que calcular")]
    public EjeDistancia ejeDistancia = EjeDistancia.Todos;
    public enum EjeDistancia
    {
        X, //solo calcula distancia en eje x
        Y, //solo calcula distancia en eje y
        Z, //solo calcula distancia en eje z
        Todos //calcula la distancia en todos los ejes
    }

    // Variable para almacenar la distancia calculada y verla en el inspector
    [Space(10)]
    [Header("Distancia")]
    public float distancia;

    // Variable para el RTPC que se actualizará con la distancia calculada
    [Header("RTPC")]
    [SerializeField]
    private AK.Wwise.RTPC rtpc = null; 


    void Update()
    {
        // Switch para determinar en qué eje calcular la distancia
        switch (ejeDistancia)
        {
            case EjeDistancia.X:
                distancia = Mathf.Abs(objeto1.position.x - objeto2.position.x);
                break;
            case EjeDistancia.Y:
                distancia = Mathf.Abs(objeto1.position.y - objeto2.position.y);
                break;
            case EjeDistancia.Z:
                distancia = Mathf.Abs(objeto1.position.z - objeto2.position.z);
                break;
            case EjeDistancia.Todos:
                distancia = Vector3.Distance(objeto1.position, objeto2.position);
                break;
        }

        // Actualiza el RTPC con el valor de la distancia calculada
        rtpc.SetGlobalValue(distancia);
    }
}
