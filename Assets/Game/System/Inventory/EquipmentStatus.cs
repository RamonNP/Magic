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
                        _defense = GetValueMediator(0, 2);
                        _magicPower = GetValueMediator(20, 26);
                        _physicalPower = GetValueMediator(3, 6);
                        break;
                    case TypeItem.Armor:
                        _mana = GetValueMediator(7, 11);
                        _life = GetValueMediator(3, 6);
                        _defense = GetValueMediator(2, 4);
                        break;
                    case TypeItem.Head:
                        _mana = GetValueMediator(22, 25);
                        _life = GetValueMediator(8, 10);
                        _defense = GetValueMediator(1, 2);
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
                        _defense = GetValueMediator(0, 2);
                        _magicPower = GetValueMediator(3, 6);
                        _physicalPower = GetValueMediator(17, 21);
                        break;
                    case TypeItem.Armor:
                        _mana = GetValueMediator(2, 5);
                        _life = GetValueMediator(7, 11);
                        _defense = GetValueMediator(2, 5);
                        break;
                    case TypeItem.Head:
                        _mana = GetValueMediator(3, 6);
                        _life = GetValueMediator(25, 30);
                        _defense = GetValueMediator(0, 1);
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
                        _magicPower = GetValueMediator(0, 2);
                        _distanceFight = GetValueMediator(20, 26);
                        break;
                    case TypeItem.Armor:
                        _mana = GetValueMediator(3, 6);
                        _life = GetValueMediator(7, 11);
                        _defense = GetValueMediator(2, 4);
                        break;
                    case TypeItem.Head:
                        _mana = GetValueMediator(17, 20);
                        _life = GetValueMediator(12, 15);
                        _defense = GetValueMediator(0, 2);
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
        //MultiplyAttributesByMediatingPower();
    }
    private int GetValueMediator(int value1, int value2)
    {
        float mediatingPower = ConfigManager.GetInstance().MediatingPowerOfItems;
        return UnityEngine.Random.Range(value1 * (int)mediatingPower, value2 * (int)mediatingPower);

    }
    public void MultiplyAttributesByMediatingPower()
    {
        float mediatingPower = ConfigManager.GetInstance().MediatingPowerOfItems;
        _life = Mathf.RoundToInt(_life * mediatingPower);
        _mana = Mathf.RoundToInt(_mana * mediatingPower);
        _physicalPower = Mathf.RoundToInt(_physicalPower * mediatingPower);
        _magicPower = Mathf.RoundToInt(_magicPower * mediatingPower);
        _defense = Mathf.RoundToInt(_defense * mediatingPower);
        _distanceFight = Mathf.RoundToInt(_distanceFight * mediatingPower);
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
