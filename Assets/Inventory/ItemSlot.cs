using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item Item;


    private void Start()
    {
        if(Item != null && !Item.IdItem.Equals(""))
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Image>().sprite = Item.SpriteItem;
        }
    }
}
