using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _mouseItem;
    [SerializeField] private List<Item> _itensInventory;
    [SerializeField] private List<Transform> _itensInventorySlots;
    [SerializeField] private List<Item> _itensRune;
    [SerializeField] private Transform[] _itensRuneSlot;
    [SerializeField] private List<Item> _itensEquipment;
    [SerializeField] private List<TypeSlot> _itensEquipmentSlot;
    [SerializeField] private Item _coin;
    public Action<Item> OnItemEquiped;
    public Action<Item> OnItemUnequip;
    public Action<Item, Item> OnChangeStatus;


    public List<Item> ItensRune { get => _itensRune; }
    public List<Item> ItensEquipment { get => _itensEquipment; }

    private void Awake()
    {
        //GameDataManager.WriteFileItemList("_itensInventory", new InventoryFileJson { ItensInventory = _itensInventory, ItensRune = _itensRune, ItensEquipment = _itensEquipment });
        InventoryFileJson inventoryFileJson = GameDataManager.ReadFileInventoryFileJson("Inventory");
        _itensInventory = inventoryFileJson.ItensInventory;
        _itensEquipment = inventoryFileJson.ItensEquipment;
        _itensRune = inventoryFileJson.ItensRune;
        _coin = inventoryFileJson.Coin;
        if(_coin.NameItem.Equals(""))
        {
            _coin = new Item();
            _coin.IdItem = "COIN001";
            _coin.NameItem = "Coin";
            _coin.TypeItem = TypeItem.Coin;
            _coin.IsGroupable = true;
        }

        RefreshInventory();

        for (int i = 0; i < _itensRune.Count; i++)
        {
            _itensRuneSlot[i].GetComponentInChildren<Image>().enabled = true;
            _itensRuneSlot[i].GetComponentInChildren<Image>().sprite = GameDataManager.GetInventorySprite(_itensRune[i]);//Resources.Load<Sprite>("ItensInventory/" + _itensRune[i].TypeItem + "/" + _itensRune[i].SpriteItem);
            _itensRuneSlot[i].GetComponentInChildren<Image>().color = Color.white;
            _itensRuneSlot[i].GetComponentInChildren<ItemSlot>().Item = _itensRune[i];
        }

        for (int i = 0; i < ItensEquipment.Count; i++)
        {
            TypeItem typeItemFind = ItensEquipment[i].TypeItem;
            if (typeItemFind.Equals(TypeItem.Bow) || typeItemFind.Equals(TypeItem.Sword))
            {
                //caso for armar, procurar arma nos Slots
                typeItemFind = TypeItem.Staff;
            }
            TypeSlot typeSlots = _itensEquipmentSlot.Find(it => it.TypeSlotItem == typeItemFind);
            typeSlots.transform.GetComponentInChildren<Image>().enabled = true;
            typeSlots.transform.GetComponentInChildren<Image>().sprite = GameDataManager.GetInventorySprite(ItensEquipment[i]);//Resources.Load<Sprite>("ItensInventory/" + ItensEquipment[i].TypeItem + "/" + ItensEquipment[i].SpriteItem);
            typeSlots.transform.GetComponentInChildren<Image>().color = Color.white;
            typeSlots.transform.GetComponentInChildren<ItemSlot>().Item = ItensEquipment[i];
        }
        

    }


    private void RefreshInventory()
    {

        int count = 0;
        for (int i = 0; i < _itensInventory.Count; i++)
        {
            Transform ItemImage = _itensInventorySlots[i].Find("ItemImage");
            ItemImage.GetComponent<Image>().enabled = true;
            ItemImage.GetComponent<Image>().color = Color.white;
            ItemImage.GetComponent<Image>().sprite = GameDataManager.GetInventorySprite(_itensInventory[i]);//Resources.Load<Sprite>("ItensInventory/" + _itensInventory[i].TypeItem + "/" + _itensInventory[i].SpriteItem.ToString());

            ItemImage.GetComponent<ItemSlot>().Item = _itensInventory[i];
            count = i;
        }
        count++;
        for (int i = count; i < _itensInventorySlots.Count; i++)
        {
            if(_itensInventorySlots[i].GetComponentInChildren<Image>().enabled)
            {
                Transform ItemImage = _itensInventorySlots[i].Find("ItemImage");
                Debug.Log("Inicializando "+i + ItemImage.GetComponent<ItemSlot>().Item.NameItem);
                ItemImage.GetComponent<ItemSlot>().Item = new Item();
                //_itensInventorySlots[i].GetChild(0).GetComponent<Image>().enabled = false;
                ItemImage.GetComponent<Image>().sprite = null; // Remove o sprite
                ItemImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f); // Define a cor como branca transparente
            }
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameDataManager.WriteFileInventoryFileJson("Inventory", new InventoryFileJson { ItensInventory = _itensInventory, ItensRune = _itensRune, ItensEquipment = ItensEquipment, Coin = _coin });
        }
    }

    public void CollectItem(Item item)
    {
        if (item.TypeItem.Equals(TypeItem.Coin))
        {
            if(_coin == null)
            {
                _coin = new Item();
                _coin.IdItem = "COIN001";
                _coin.NameItem = "Coin";
                _coin.TypeItem = TypeItem.Coin;
            }
            _coin.Count++;
            return;
        }
        if(_itensInventory.Count < 15)
        {
            int i = _itensInventory.Count;
            _itensInventory.Add(item);
            Debug.Log("ADICIONADO AO INVENTARIO - Slot "+i+" Name "+item.NameItem);
            _itensInventorySlots[i].GetComponentInChildren<Image>().enabled = true;
            _itensInventorySlots[i].GetComponentInChildren<Image>().color = Color.white;
            _itensInventorySlots[i].GetComponentInChildren<Image>().sprite = GameDataManager.GetInventorySprite(_itensInventory[i]);//Resources.Load<Sprite>("ItensInventory/" + _itensInventory[i].TypeItem + "/" + _itensInventory[i].SpriteItem.ToString());
            _itensInventorySlots[i].GetComponentInChildren<ItemSlot>().Item = item;
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
            TypeSlot targetType = button.transform.parent.GetComponent<TypeSlot>();
            if(targetType.TypeSlotItem.Equals(TypeItem.Drop))
            {
                _mouseItem.transform.localPosition = Vector3.zero;
                _itensInventory.Remove(_mouseItem.transform.GetComponent<ItemSlot>().Item);
                RefreshInventory();
                return;
            }
            //Debug.Log("type.TypeSlotItem " + targetType.TypeSlotItem + " _mouseItem.GetComponent<ItemSlot>() " + _mouseItem.GetComponent<ItemSlot>().Item.TypeItem);
            //set save Change Itens List

            if (( targetType.TypeSlotItem.Equals(TypeItem.Alls)
                || isShieldOrMagicIten(targetType)
                || (isWeapomIten(_mouseItem.GetComponent<ItemSlot>()) && targetType.TypeSlotItem.Equals(TypeItem.Staff))
                || targetType.TypeSlotItem.Equals(_mouseItem.GetComponent<ItemSlot>().Item.TypeItem))
                || CheckItemCompatibility(_mouseItem, button))
            {
                Debug.Log("type.TypeSlotItem " + targetType.TypeSlotItem + " _mouseItem.GetComponent<ItemSlot>() " + _mouseItem.GetComponent<ItemSlot>().Item.TypeItem);
                //set save Change Itens List
                ChangeItemSlots(_mouseItem.transform.parent.GetComponent<TypeSlot>().TypeListItem, targetType.TypeListItem, _mouseItem.transform.GetComponent<ItemSlot>().Item);
                ChangeItemSlots(targetType.TypeListItem, _mouseItem.transform.parent.GetComponent<TypeSlot>().TypeListItem, targetType.GetComponentInChildren<ItemSlot>().Item);

                if (CheckItemCompatibility(_mouseItem, button))
                {
                    OnChangeStatus?.Invoke(targetType.GetComponentInChildren<ItemSlot>().Item, _mouseItem.transform.GetComponent<ItemSlot>().Item);
                }

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
                //RefreshInventory();
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
    private bool isWeapomIten(ItemSlot type)
    {
        return (
            (type.Item.TypeItem.Equals(TypeItem.Staff) || type.Item.TypeItem.Equals(TypeItem.Sword) || type.Item.TypeItem.Equals(TypeItem.Bow)) 
            );
    }


    public bool CheckItemCompatibility(GameObject item, GameObject target)
    {
        var itemSlotType = item.transform.parent.GetComponent<TypeSlot>().TypeSlotItem;
        var itemIsWeapon = isWeapomIten(item.transform.GetComponent<ItemSlot>());
        var targetIsWeapon = isWeapomIten(target.transform.GetComponent<ItemSlot>());
        var itemIsCompatibleWithTargetType = itemSlotType.Equals(target.GetComponent<ItemSlot>().Item.TypeItem);

        if (itemIsCompatibleWithTargetType)
        {
            return true;
        }

        if (itemIsWeapon && targetIsWeapon)
        {
            return true;
        }

        return false;
    }
    public bool getItemSlot(GameObject iten, GameObject target)
    {
        if(target.transform.parent.GetComponent<TypeSlot>().TypeSlotItem.Equals(TypeItem.Alls) && iten.transform.parent.GetComponent<TypeSlot>().TypeSlotItem.Equals(TypeItem.Alls))
        {
            return true;
        } 
        else if (target.transform.parent.GetComponent<TypeSlot>().TypeSlotItem.Equals(TypeItem.Alls) && iten.GetComponent<ItemSlot>().Item.TypeItem.Equals(target.GetComponent<ItemSlot>().Item.TypeItem))
        {
            return true;
        }        
        else if (target.transform.parent.GetComponent<TypeSlot>().TypeSlotItem.Equals(TypeItem.Alls) 
            && isWeapomIten(target.transform.GetComponent<ItemSlot>())
            && isWeapomIten(iten.transform.GetComponent<ItemSlot>())
            )
        {
            return true;
        }
        //return target.GetComponentInChildren<ItemSlot>().Item.TypeItem.Equals(TypeItem.None);
        return false;
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
    public Item Coin;

}
