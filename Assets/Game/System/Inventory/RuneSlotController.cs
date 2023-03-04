using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSlotController : MonoBehaviour
{
    [SerializeField] private PowerRune _powerRune;
    [SerializeField] private Item _itemSlotEquiped;

    public PowerRune PowerRune { get => _powerRune; set => _powerRune = value; }
    public void SetItemSlot(Item item)
    {
        _itemSlotEquiped = item;
        _powerRune = item.PowerRuneItem;
    }
}
