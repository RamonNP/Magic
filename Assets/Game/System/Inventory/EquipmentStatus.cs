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

    public void GenerateEquipmentStatus(ClassItem classItem, TypeItem typeItem)
    {
        switch (classItem)
        {
            case ClassItem.Mage:
                switch (typeItem)
                {
                    case TypeItem.Staff:
                        _defense = UnityEngine.Random.Range(0, 2);
                        _magicPower = UnityEngine.Random.Range(20, 26);
                        _physicalPower = UnityEngine.Random.Range(3, 6);
                        break;
                    case TypeItem.Armor:
                        _mana = UnityEngine.Random.Range(7, 11);
                        _life = UnityEngine.Random.Range(3, 6);
                        _defense = UnityEngine.Random.Range(2, 4);
                        break;
                    default:
                        Debug.LogError("Invalid TypeItem for Mage class");
                        break;
                }
                break;
            case ClassItem.Warrior:
                switch (typeItem)
                {
                    case TypeItem.Sword:
                        _defense = UnityEngine.Random.Range(0, 2);
                        _magicPower = UnityEngine.Random.Range(3, 6);
                        _physicalPower = UnityEngine.Random.Range(17, 21);
                        break;
                    case TypeItem.Armor:
                        _mana = UnityEngine.Random.Range(2, 5);
                        _life = UnityEngine.Random.Range(7, 11);
                        _defense = UnityEngine.Random.Range(2, 5);
                        break;
                    default:
                        Debug.LogError("Invalid TypeItem for Warrior class");
                        break;
                }
                break;
            case ClassItem.Archer:
                switch (typeItem)
                {
                    case TypeItem.Bow:
                        _magicPower = UnityEngine.Random.Range(0, 2);
                        _distanceFight = UnityEngine.Random.Range(20, 26);
                        break;
                    case TypeItem.Armor:
                        _mana = UnityEngine.Random.Range(3, 6);
                        _life = UnityEngine.Random.Range(7, 11);
                        _defense = UnityEngine.Random.Range(2, 4);
                        break;
                    default:
                        Debug.LogError("Invalid TypeItem for Archer class");
                        break;
                }
                break;
            default:
                Debug.LogError("Invalid ClassItem");
                break;
        }
    }

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
   None,
   SetOfEnts
}
