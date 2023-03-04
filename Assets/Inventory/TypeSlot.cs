using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSlot : MonoBehaviour
{
    [SerializeField] private TypeItem _typeSlotItem;
    [SerializeField] private TypeListItem _typeListItem;

    public TypeItem TypeSlotItem { get => _typeSlotItem; set => _typeSlotItem = value; }
    public TypeListItem TypeListItem { get => _typeListItem; set => _typeListItem = value; }
}
