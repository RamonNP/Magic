using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Weapon
{
    public string name;
    public int damage;
    public float attackSpeed;
    public float range;
    public Sprite sprite;
    public WeaponCategory category;

    public Weapon(string name, int damage, float attackSpeed, float range, Sprite sprite, WeaponCategory category)
    {
        this.name = name;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.range = range;
        this.sprite = sprite;
        this.category = category;
    }
}
public enum WeaponCategory
{
    Sword,
    Staff,
    Bow
}
