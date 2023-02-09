using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private GameObject _mouseItem;
    [SerializeField] private Item[] _itensInventory;
    [SerializeField] private Transform[] _itensInventorySlots;
    [SerializeField] private Item[] _itensRune;
    [SerializeField] private Transform[] _itensRuneSlot;
    [SerializeField] private Item[] _itensEquipment;
    [SerializeField] private Transform[] _itensEquipmentSlot;

    private void Awake()
    {
        foreach (Item item in _itensInventory)
        {
            _itensInventorySlots[item.SlotInventory].GetChild(0).GetComponent<Image>().enabled = true;
            _itensInventorySlots[item.SlotInventory].GetChild(0).GetComponent<Image>().color = Color.white;
            _itensInventorySlots[item.SlotInventory].GetChild(0).GetComponent<Image>().sprite = item.SpriteItem;
            _itensInventorySlots[item.SlotInventory].GetChild(0).GetComponent<ItemSlot>().Item = item;
        }

    }

    public void DragItem(GameObject button)
    {
        _mouseItem = button;
        _mouseItem.transform.position = Input.mousePosition;
    }    
    public void DropItem(GameObject button)
    {
        if(_mouseItem != null)
        {

            Transform auxParent = _mouseItem.transform.parent;
            TypeSlot type = button.transform.parent.GetComponent<TypeSlot>();
            if (type.TypeSlotItem.Equals(TypeItem.Alls) || type.TypeSlotItem.Equals(_mouseItem.GetComponent<ItemSlot>().Item.TypeItem))
            {
                Vector2 auxSize = _mouseItem.transform.GetComponent<RectTransform>().sizeDelta;
                Debug.Log(auxSize);
                _mouseItem.transform.GetComponent<RectTransform>().sizeDelta = button.transform.GetComponent<RectTransform>().sizeDelta;
                button.transform.GetComponent<RectTransform>().sizeDelta = auxSize;
                Debug.Log(auxSize);


                _mouseItem.transform.SetParent(button.transform.parent);
                button.transform.SetParent(auxParent);
            }
        }
    }
}
