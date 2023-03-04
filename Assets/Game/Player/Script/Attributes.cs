using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attributes
{
    public int ManaMax;
    [SerializeField] private int manaCurrent;
    [SerializeField] private int lifeCurrent;
    public int LifeMax;
    public int ATKBase = 100;
    public int DEFBase = 50;         
    public int ManaBase = 100;     
    public int LifeBase = 100;     
    private int _lifePerLevel = 25;     
    private int _manaPerLevel = 50;     
    public int ATK ;
    public int DEF ;    
    public int BonusEquipmentATK;
    public int BonusEquipmentDEF;
    public bool BufferArmorComplet;
    public int BufferItemArmor;
    public int Level;
    public int XP;
    public int XPNexLevel;
    [SerializeField] private int _runeBonus;

    public void Initialize()
    {
        if (ATK == 0)
        {
            ATK = 100;
        }
        if (DEF == 0)
        {
            DEF = 50;
        }
        BonusEquipmentATK = 0;
        BonusEquipmentDEF = 0;
        LifeMax = LifeBase + (_lifePerLevel * Level);
        ManaMax = ManaBase + (_manaPerLevel * Level);
        lifeCurrent = LifeMax;
        manaCurrent = ManaMax;

    }
    public void EquipItem(Item item)
    {
        BonusEquipmentATK += item.EquipmentStatus.ATK;
        BonusEquipmentDEF += item.EquipmentStatus.DEF;
        CalculateATK();
    }


    public void UnequipItem(Item item)
    {
        BonusEquipmentATK -= item.EquipmentStatus.ATK;
        BonusEquipmentDEF -= item.EquipmentStatus.DEF;
        CalculateATK();
    }    
    public void CalculateATK()
    {
        ATK = ATKBase;
        ATK += (ATKBase * BonusEquipmentATK) / 100;
        ATK += (ATKBase * _runeBonus) / 100;
        CalculateDEF();
    }    
    private void CalculateDEF()
    {
        DEF = DEFBase;
        DEF += (DEFBase * BonusEquipmentDEF) / 100;
        DEF += (DEFBase * _runeBonus) / 100;
    }
    public void AddRuneBonus(float magicLevel)
    {
        _runeBonus += ((int)magicLevel);
        CalculateATK();
    }    
    public void RemoveRuneBonus(float magicLevel)
    {
        _runeBonus -= ((int)magicLevel);
        CalculateATK();
    }    
    public void CastMana(int  amount)
    {
        manaCurrent -= amount;
    }    
    public void LoseLife(int  amount)
    {
        lifeCurrent -= amount;
    }
    public int RuneBonus { get => _runeBonus; set => _runeBonus = value; }
    public int ManaCurrent { get => manaCurrent; }
    public int LifeCurrent { get => lifeCurrent; }
}
