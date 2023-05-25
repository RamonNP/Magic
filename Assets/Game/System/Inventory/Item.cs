using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item 
{
    public string IdItem;
    public string NameItem;
    public TypeItem TypeItem;
    [SerializeField] private bool _isGroupable;
    [SerializeField] private int _count = 1;
    public int SpriteItem = 1;
    public ClassItem classItem;
    public PowerRune PowerRuneItem;
    public EquipmentStatus EquipmentStatus;
    public int Count { get => _count; set => _count = value; }
    public bool IsGroupable { get => _isGroupable; set => _isGroupable = value; }

    public override bool Equals(object obj)
    {
        return obj is Item item &&
               IdItem == item.IdItem &&
               NameItem == item.NameItem &&
               TypeItem == item.TypeItem &&
               SpriteItem == item.SpriteItem;
    }

    public void GenereteRune()
    {
        int newSlots = Random.Range(0, 100);
        if(newSlots > 90)
        {
            newSlots = 3;
        }        
        if(newSlots > 70 && newSlots < 90)
        {
            newSlots = 2;
        }
        if (newSlots <= 70)
        {
            newSlots = 1;
        }
        PowerRuneItem = new();
        PowerRuneItem.Name = "TesteCriarAleatorio";
        PowerRuneItem.Magics = new();
        PowerRuneItem.SlotsMagic = newSlots;
        for (int i = 0; i < newSlots; i++)
        {
            //Random.Range(minSlot, maxSlot)
            MagicEnum magic = (MagicEnum)Random.Range(0, 8);
            PowerRuneItem.Magics.Add(magic);
        }
    }

    public void InitializeVariables()
    {
        IdItem = "";
        NameItem = "";
        TypeItem = TypeItem.None;
        SpriteItem = 0;
        classItem = ClassItem.None;
        PowerRuneItem = new PowerRune();
        EquipmentStatus = new EquipmentStatus();
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(IdItem, NameItem, TypeItem, SpriteItem);
    }
}
public enum TypeItem
{
    None,
    Armor,
    Rune,
    Staff,
    Head,
    Boots,
    Shield,
    MagicItem,
    Amulet,
    Alls, 
    Sword,
    Bow,
    Coin,
    Drop
}
public enum TypeListItem
{
    Inventory,
    Equipment,
    Runes,
    None,
    Drop
}
public enum ClassItem
{
    None,
    Mage,
    Warrior,
    Archer
}
