using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item 
{

    public string IdItem;
    public string NameItem;
    public TypeItem TypeItem;
    public Sprite SpriteItem;
    public int SlotInventory;

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
    Amulet,
    Alls
}
