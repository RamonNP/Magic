using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item Item;
    public Action<Item> OnRuneEquiped;


    private void Start()
    {
        //CheckItemTypeAndSetSize();
        if (Item != null && !"".Equals(Item.IdItem))
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            string path = "ItensInventory/" + Item.TypeItem + "/" + Item.SpriteItem;
            if (Item.TypeItem.Equals(TypeItem.Armor))
            {
                path = "ItensInventory/" + Item.TypeItem + "/icon" + Item.SpriteItem;
            }
            Debug.Log(path);
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(path); 
        }
    }

    public void CheckItemTypeAndSetSize()
    {
        var itemSlot = GetComponent<ItemSlot>();
        var rectTransform = GetComponent<RectTransform>();
        if (itemSlot != null &&
            (itemSlot.Item.TypeItem.Equals(TypeItem.Sword) ||
             itemSlot.Item.TypeItem.Equals(TypeItem.Staff) ||
             itemSlot.Item.TypeItem.Equals(TypeItem.Bow)))
        {
            Debug.Log(itemSlot.Item.NameItem + new Vector2(80, 80));
            rectTransform.sizeDelta = new Vector2(80, 80);
        } else if(!itemSlot.Item.TypeItem.Equals(TypeItem.None))
        {
            //Debug.Log(itemSlot.Item.NameItem + new Vector2(180, 90));
            //rectTransform.sizeDelta = new Vector2(180, 90);
        }
    }

    public void ChangeSlotRune()
    {
        OnRuneEquiped?.Invoke(Item);
    }
}
