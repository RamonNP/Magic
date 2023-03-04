using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerRune
{
    public string Name;
    public int SlotsMagic;
    public int MagicLevel;
    public int XPCurrent;
    [SerializeField]private int _XPBase = 100;

    public List<MagicEnum> Magics;

    public int XPBase { get => _XPBase; }


    public int XPNexLevel()// retorna o valor da coluna xp, sempre 25% a mais para o proximo level.
    {
        float xpLevel = 0;
        for (int i = 0; i < MagicLevel; i++)
        {
            xpLevel += xpLevel+(xpLevel * 0.25f);
        }
        return (int)xpLevel;
    }
}
