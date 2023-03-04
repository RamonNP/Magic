using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentStatus 
{
    public int ATK;
    public int DEF;
    public EquipmentSet EquipmentSet;
}

public enum EquipmentSet
{
   SetOfEnts,
   None
}
