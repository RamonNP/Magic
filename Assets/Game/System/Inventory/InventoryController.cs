using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _mouseItem;
    [SerializeField] private List<Item> _itensInventory;
    [SerializeField] private Transform[] _itensInventorySlots;
    [SerializeField] private List<Item> _itensRune;
    [SerializeField] private Transform[] _itensRuneSlot;
    [SerializeField] private List<Item> _itensEquipment;
    [SerializeField] private List<TypeSlot> _itensEquipmentSlot;
    public Action<Item> OnItemEquiped;
    public Action<Item> OnItemUnequip;

    public List<Item> ItensRune { get => _itensRune; }
    public List<Item> ItensEquipment { get => _itensEquipment; }

    private void Awake()
    {
        //GameDataManager.WriteFileItemList("_itensInventory", new InventoryFileJson { ItensInventory = _itensInventory, ItensRune = _itensRune, ItensEquipment = _itensEquipment });
         InventoryFileJson inventoryFileJson = GameDataManager.ReadFileInventoryFileJson("Inventory");
        _itensInventory = inventoryFileJson.ItensInventory;
        _itensEquipment = inventoryFileJson.ItensEquipment;
        _itensRune = inventoryFileJson.ItensRune;


        for (int i = 0; i < _itensInventory.Count; i++)
        {
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().enabled = true;
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().color = Color.white;
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItensInventory/" + _itensInventory[i].TypeItem + "/" + _itensInventory[i].SpriteItem.ToString());
            _itensInventorySlots[i].GetChild(0).GetComponent<ItemSlot>().Item = _itensInventory[i];
            /*
            if (_itensInventory[i].TypeItem.Equals(TypeItem.Rune))
            {
                _itensInventory[i].GenereteRune();
            } */
        }        
        
        for (int i = 0; i < _itensRune.Count; i++)
        {
            _itensRuneSlot[i].GetChild(0).GetComponent<Image>().enabled = true;
            _itensRuneSlot[i].GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItensInventory/" + _itensRune[i].TypeItem +"/"+ _itensRune[i].SpriteItem);
            _itensRuneSlot[i].GetChild(0).GetComponent<Image>().color = Color.white;
            _itensRuneSlot[i].GetChild(0).GetComponent<ItemSlot>().Item = _itensRune[i];
        }        
        for (int i = 0; i < ItensEquipment.Count; i++)
        {
            //_itensEquipment.FindAll(it-> it.)
            //Debug.Log("Procurando"+ItensEquipment[i].TypeItem);
            TypeItem typeItemFind = ItensEquipment[i].TypeItem;
            if (typeItemFind.Equals(TypeItem.Bow) || typeItemFind.Equals(TypeItem.Sword))
            {
                //caso for armar, procurar arma nos Slots
                typeItemFind = TypeItem.Staff;
            }
            TypeSlot typeSlots = _itensEquipmentSlot.Find(it => it.TypeSlotItem == typeItemFind);
            //ChangeItemSlots(TypeListItem.None, typeSlots.TypeListItem, _itensEquipment[i]);
            typeSlots.transform.GetChild(0).GetComponent<Image>().enabled = true;
            typeSlots.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItensInventory/" + ItensEquipment[i].TypeItem + "/" + ItensEquipment[i].SpriteItem);
            typeSlots.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            typeSlots.transform.GetChild(0).GetComponent<ItemSlot>().Item = ItensEquipment[i];
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameDataManager.WriteFileInventoryFileJson("Inventory", new InventoryFileJson { ItensInventory = _itensInventory, ItensRune = _itensRune, ItensEquipment = ItensEquipment });
        }
    }

    public void CollectItem(Item item)
    {
        if(_itensInventory.Count < 16)
        {
            int i = _itensInventory.Count;
            _itensInventory.Add(item);
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().enabled = true;
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().color = Color.white;
            _itensInventorySlots[i].GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ItensInventory/" + _itensInventory[i].TypeItem + "/" + _itensInventory[i].SpriteItem.ToString());
            _itensInventorySlots[i].GetChild(0).GetComponent<ItemSlot>().Item = item;
            Debug.Log("ADICIONADO AO INVENTARIO - "+item.NameItem);
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

            if ((type.TypeSlotItem.Equals(TypeItem.Alls)
                || isShieldOrMagicIten(type)
                || isWeapomIten(type)
                || type.TypeSlotItem.Equals(_mouseItem.GetComponent<ItemSlot>().Item.TypeItem))
                && getItemSlot(type).Item.TypeItem.Equals(TypeItem.None))
            {
                Debug.Log("type.TypeSlotItem " + type.TypeSlotItem + " _mouseItem.GetComponent<ItemSlot>() " + _mouseItem.GetComponent<ItemSlot>().Item.TypeItem);
                //set save Change Itens List
                ChangeItemSlots(_mouseItem.transform.parent.GetComponent<TypeSlot>().TypeListItem, type.TypeListItem, _mouseItem.transform.GetComponent<ItemSlot>().Item);
                if (type.TypeSlotItem.Equals(_mouseItem.GetComponent<ItemSlot>().Item.TypeItem))
                    ChangeItemSlots(type.TypeListItem, _mouseItem.transform.parent.GetComponent<TypeSlot>().TypeListItem, getItemSlot(type).Item);

                Vector2 auxSize = _mouseItem.transform.GetComponent<RectTransform>().sizeDelta;
                _mouseItem.transform.GetComponent<RectTransform>().sizeDelta = button.transform.GetComponent<RectTransform>().sizeDelta;
                button.transform.GetComponent<RectTransform>().sizeDelta = auxSize;
                //Debug.Log(auxSize);

                //set Slot Extract
                RuneSlotController runeSlotController = button.transform.parent.GetComponent<RuneSlotController>();
                if (runeSlotController != null)
                {
                    ItemSlot itemSlotCurrent = _mouseItem.transform.GetComponent<ItemSlot>();
                    itemSlotCurrent.OnRuneEquiped += runeSlotController.SetItemSlot;
                    itemSlotCurrent.ChangeSlotRune();
                }
                // End

                _mouseItem.transform.SetParent(button.transform.parent);
                button.transform.SetParent(auxParent);
            }
        }
    }

    private bool isShieldOrMagicIten(TypeSlot type)
    {
        TypeItem currentItemTypeItem = _mouseItem.GetComponent<ItemSlot>().Item.TypeItem;
        return ((type.TypeSlotItem.Equals(TypeItem.Shield)) 
            && (currentItemTypeItem.Equals(TypeItem.Shield) 
                    || currentItemTypeItem.Equals(TypeItem.MagicItem)));
    }    
    private bool isWeapomIten(TypeSlot type)
    {
        TypeItem currentItemTypeItem = _mouseItem.GetComponent<ItemSlot>().Item.TypeItem;
        return ((type.TypeSlotItem.Equals(TypeItem.Staff)) 
            && (currentItemTypeItem.Equals(TypeItem.Staff) 
                    || currentItemTypeItem.Equals(TypeItem.Sword)
                    || currentItemTypeItem.Equals(TypeItem.Bow)
                    ));
    }

    public ItemSlot getItemSlot(TypeSlot itemSlot)
    {
        return itemSlot.GetComponentInChildren<ItemSlot>();
    }
    public void ChangeItemSlots(TypeListItem origem, TypeListItem target, Item item)
    {
        if(item.TypeItem.Equals(TypeItem.None))
        {
            return;
        }
        //Debug.Log(origem + "-" + target + "-" + item.NameItem);
        if(origem.Equals(TypeListItem.Inventory))
        {
            _itensInventory.Remove(item);
        } 
        else if (origem.Equals(TypeListItem.Equipment))
        {
            ItensEquipment.Remove(item);
        }
        else if (origem.Equals(TypeListItem.Runes))
        {
            _itensRune.Remove(item);
        }        
        if(target.Equals(TypeListItem.Inventory))
        {
            _itensInventory.Add(item);
        } 
        else if (target.Equals(TypeListItem.Equipment))
        {
            ItensEquipment.Add(item);
        }
        else if (target.Equals(TypeListItem.Runes))
        {
            _itensRune.Add(item);
        }
        if(origem.Equals(TypeListItem.Equipment) || origem.Equals(TypeListItem.Runes)) {
            OnItemUnequip?.Invoke(item);
            //Debug.Log(origem + "-" + target + "-" + item.NameItem);
        }
        if (target.Equals(TypeListItem.Equipment) || target.Equals(TypeListItem.Runes))
        {
            OnItemEquiped?.Invoke(item);
        }
    }
}

public class InventoryFileJson
{
    public List<Item> ItensInventory;
    public List<Item> ItensRune;
    public List<Item> ItensEquipment;


}
