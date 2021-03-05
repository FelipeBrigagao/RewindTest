using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class ObjectsDataSO : ScriptableObject
{

    public new string name;
    public float interactionRadius;
    public ObjectType objectype;


    public enum ObjectType {Breakable, Trowable, Usable, Normal};


}
