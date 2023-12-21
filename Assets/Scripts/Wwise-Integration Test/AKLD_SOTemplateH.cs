using UnityEngine;

[CreateAssetMenu(menuName = "SO/Audio/Player", fileName = "New Player Sheet")]
public class AKLD_SOTemplateH : ScriptableObject
{
    [Header("Movement")]

    public AK.Wwise.Event footsteps = null;


    [Header("Attacks")]

    public AK.Wwise.Event swordAttack = null;


    [Header("Rtpc")]

    public AK.Wwise.RTPC test1 = null;



    [Header("state")]

    public AK.Wwise.Switch Switch = null; 


    [Header("State")]

    public AK.Wwise.State State = null; 

}
