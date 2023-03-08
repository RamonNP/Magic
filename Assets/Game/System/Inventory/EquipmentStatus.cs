using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentStatus 
{
    [SerializeField] private int _life;
    [SerializeField] private int _mana;
    [SerializeField] private int _physicalPower;
    [SerializeField] private int _magicPower;
    [SerializeField] private int _defense;
    [SerializeField] private int _distanceFight;
    [SerializeField] public EquipmentSet EquipmentSet;

    public int Life
    {
        get { return _life; }
        set { _life = value; }
    }

    public int Mana
    {
        get { return _mana; }
        set { _mana = value; }
    }

    public int PhysicalPower
    {
        get { return _physicalPower; }
        set { _physicalPower = value; }
    }

    public int MagicPower
    {
        get { return _magicPower; }
        set { _magicPower = value; }
    }

    public int Defense
    {
        get { return _defense; }
        set { _defense = value; }
    }

    public int DistanceFight
    {
        get { return _distanceFight; }
        set { _distanceFight = value; }
    }
}

public enum EquipmentSet
{
   SetOfEnts,
   None
}
