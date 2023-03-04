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
        if(Item != null && !Item.IdItem.Equals(""))
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItensInventory/"+ Item.TypeItem + "/" +Item.SpriteItem); 
        }
    }

    public void ChangeSlotRune()
    {
        OnRuneEquiped?.Invoke(Item);
    }
}
