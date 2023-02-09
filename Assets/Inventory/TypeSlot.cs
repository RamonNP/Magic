using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSlot : MonoBehaviour
{
    [SerializeField] private TypeItem _typeSlotItem;

    public TypeItem TypeSlotItem { get => _typeSlotItem; set => _typeSlotItem = value; }
}
