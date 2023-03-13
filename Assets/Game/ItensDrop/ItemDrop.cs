using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Item { get => _item;}

    private void Start()
    {
        Item.EquipmentStatus.GenerateEquipmentStatus(_item.classItem, _item.TypeItem);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            PlayerStatusController player = collision.gameObject.GetComponent<PlayerStatusController>();
            player.InventoryController.CollectItem(_item);
            Destroy(gameObject);
        }
    }
}
