using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    [SerializeField] private bool _isflying;
    [SerializeField] private int _manaBse;
    [SerializeField] private int _LifeBase;
    [SerializeField] private Attributes attributes;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private List<PowerRune> _powerRuneListEquiped;
    [SerializeField] private List<Transform> _itensEquipmentSpritePlayer;
    [SerializeField] private Arrow _equipedArrow;
    [SerializeField] private GameObject _weapomEquiped;
    [SerializeField] private GameObject _bowEquiped;



    [Header("UI Text Config")]
    [SerializeField] private RectTransform _manaCurrentBar;
    [SerializeField] private RectTransform _lifeCurrentBar;
    [SerializeField] private GameObject _magicHud;
    [SerializeField] private GameObject _warriorHud;
    [SerializeField] private GameObject _distanceHud;

    private void Start()
    {
        //attributes = GameDataManager.ReadFileAttributesFileJson("PlayerAtributs");
        Attributes attributesAux = GameDataManager.ReadFileAttributesFileJson("PlayerAtributs");
        if(attributesAux != null)
        {
            attributes = attributesAux;
        }
        InitializeAtributes();

        foreach (var item in _inventoryController.ItensRune)
        {
            EquipedItem(item);
        }        
        foreach (var item in _inventoryController.ItensEquipment)
        {
            EquipedItem(item);
        }
    }

    private void InitializeAtributes()
    {
        attributes.Initialize();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameDataManager.WriteFileAttributesFileJson("PlayerAtributs", attributes);
        }
    }

    public Action<bool> OnplayerFlying;

    private void OnEnable()
    {
        _inventoryController.OnItemEquiped += EquipedItem;
        _inventoryController.OnItemUnequip += UnequipItem;
        _inventoryController.OnChangeStatus += UpdateStatus;
    }

    private void UpdateStatus(Item oldItem, Item newItem)
    {
        Debug.Log("TRADE");
        Attributes oldAttributes = attributes.Clone();
        oldAttributes.UnequipItem(newItem);
        oldAttributes.EquipItem(oldItem);
        oldAttributes = oldAttributes.Clone();

        this.transform.GetComponent<UIAttributes>().UpdateStatus(oldAttributes);
        this.transform.GetComponent<UIAttributes>().UpdateStatus(attributes);
    }

    private void EquipedItem(Item item)
    {
        if ((item.TypeItem.Equals(TypeItem.Staff) || item.TypeItem.Equals(TypeItem.Bow) || item.TypeItem.Equals(TypeItem.Sword)))
        {
            ActiveHud(item);
        }
        if (item.TypeItem.Equals(TypeItem.Rune))
        {
            _powerRuneListEquiped.Add(item.PowerRuneItem);
            attributes.AddRuneBonus(item.PowerRuneItem.MagicLevel);
        }        
        if(!item.TypeItem.Equals(TypeItem.Rune) && !item.TypeItem.Equals(TypeItem.None) && !item.TypeItem.Equals(TypeItem.Alls))
        {
            attributes.EquipItem(item);
            ChangeSpriteEquipArmor(item);
        }

        this.transform.GetComponent<UIAttributes>().UpdateStatus(attributes);

    }
    private void ChangeSpriteEquipArmor(Item item)
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("ItensInventory/" + item.TypeItem + "/" + item.SpriteItem.ToString());
        if (item.TypeItem.Equals(TypeItem.Armor))
        {
            _itensEquipmentSpritePlayer[0].GetComponent<SpriteRenderer>().sprite = allSprites[0];
            _itensEquipmentSpritePlayer[1].GetComponent<SpriteRenderer>().sprite = allSprites[1];
            _itensEquipmentSpritePlayer[2].GetComponent<SpriteRenderer>().sprite = allSprites[2];
            _itensEquipmentSpritePlayer[3].GetComponent<SpriteRenderer>().sprite = allSprites[3];
            _itensEquipmentSpritePlayer[4].GetComponent<SpriteRenderer>().sprite = allSprites[4];
        } 
        else if (item.TypeItem.Equals(TypeItem.Head))
        {
            _itensEquipmentSpritePlayer[5].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        } 
        else if (item.TypeItem.Equals(TypeItem.Shield))
        {
            _itensEquipmentSpritePlayer[6].gameObject.SetActive(true);
            _itensEquipmentSpritePlayer[7].gameObject.SetActive(false);
            _itensEquipmentSpritePlayer[6].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        }
        else if (item.TypeItem.Equals(TypeItem.MagicItem))
        {
            _itensEquipmentSpritePlayer[6].gameObject.SetActive(false);
            _itensEquipmentSpritePlayer[7].gameObject.SetActive(true);
            _itensEquipmentSpritePlayer[7].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        }  
        else if(item.TypeItem.Equals(TypeItem.Bow))
        {
            _itensEquipmentSpritePlayer[8].gameObject.SetActive(false);
            _itensEquipmentSpritePlayer[9].gameObject.SetActive(true);
            _itensEquipmentSpritePlayer[9].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        }
        else if (item.TypeItem.Equals(TypeItem.Staff) || item.TypeItem.Equals(TypeItem.Sword))
        {
            _itensEquipmentSpritePlayer[8].gameObject.SetActive(true);
            _itensEquipmentSpritePlayer[9].gameObject.SetActive(false);
            _itensEquipmentSpritePlayer[8].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        } 
    }

    private void ActiveHud(Item item)
    {
        if (item.TypeItem.Equals(TypeItem.Staff))
        {
            _weapomEquiped.SetActive(true);
            _bowEquiped.SetActive(false);
            _magicHud.SetActive(true);
            _warriorHud.SetActive(false);
            _distanceHud.SetActive(false);
        }
        else if (item.TypeItem.Equals(TypeItem.Bow))
        {
            _bowEquiped.SetActive(true);
            _weapomEquiped.SetActive(false);
            _distanceHud.SetActive(true);
            _magicHud.SetActive(false);
            _warriorHud.SetActive(false);
        }
        else if (item.TypeItem.Equals(TypeItem.Sword))
        {
            _weapomEquiped.SetActive(true);
            _bowEquiped.SetActive(false);
            _warriorHud.SetActive(true);
            _distanceHud.SetActive(false);
            _magicHud.SetActive(false);
        }
    }

    private void CastMana(float amount)
    {
        attributes.CastMana((int)amount);
        _manaCurrentBar.localScale = new Vector3((float)attributes.ManaCurrent / (float)attributes.ManaMax, 1, 1);
    }    
    
    private void LoseLife(float amount)
    {
        //attributes.LoseLife((int)amount);
        _lifeCurrentBar.localScale = new Vector3((float)attributes.LifeCurrent / (float)attributes.LifeMax, 1, 1);
    }

    private void UnequipItem(Item item)
    {
        if (item.TypeItem.Equals(TypeItem.Rune))
        {
            Debug.Log("UnequipRune "+item.TypeItem + "-" + item.NameItem + "-" + item.PowerRuneItem.MagicLevel);
            _powerRuneListEquiped.Remove(item.PowerRuneItem);
            attributes.RemoveRuneBonus(item.PowerRuneItem.MagicLevel);
        }
        if (!item.TypeItem.Equals(TypeItem.Rune) && !item.TypeItem.Equals(TypeItem.None) && !item.TypeItem.Equals(TypeItem.Alls))
        {
            Debug.Log("UnequipItem - " + item.NameItem);

            attributes.UnequipItem(item);
            ChangeSpriteUnEquipArmor(item);
        }
        this.transform.GetComponent<UIAttributes>().UpdateStatus(attributes);
    }

    private void ChangeSpriteUnEquipArmor(Item item)
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("ItensInventory/" + item.TypeItem + "/0");
        if (item.TypeItem.Equals(TypeItem.Armor))
        {
            _itensEquipmentSpritePlayer[0].GetComponent<SpriteRenderer>().sprite = allSprites[0];
            _itensEquipmentSpritePlayer[1].GetComponent<SpriteRenderer>().sprite = allSprites[1];
            _itensEquipmentSpritePlayer[2].GetComponent<SpriteRenderer>().sprite = allSprites[2];
            _itensEquipmentSpritePlayer[3].GetComponent<SpriteRenderer>().sprite = allSprites[3];
            _itensEquipmentSpritePlayer[4].GetComponent<SpriteRenderer>().sprite = allSprites[4];
        }
        else if (item.TypeItem.Equals(TypeItem.Head))
        {
            _itensEquipmentSpritePlayer[5].GetComponent<SpriteRenderer>().sprite = allSprites[0];
        }
        else if (item.TypeItem.Equals(TypeItem.Shield) || item.TypeItem.Equals(TypeItem.MagicItem))
        {
            _itensEquipmentSpritePlayer[6].gameObject.SetActive(false);
            _itensEquipmentSpritePlayer[7].gameObject.SetActive(false);
        }
        else if (item.TypeItem.Equals(TypeItem.Staff) || item.TypeItem.Equals(TypeItem.Bow) || item.TypeItem.Equals(TypeItem.Sword))
        {
            _itensEquipmentSpritePlayer[8].gameObject.SetActive(false);
        }
    }

    public bool Isflying { get => _isflying; }
    public Attributes Attributes { get => attributes; set => attributes = value; }
    public Arrow EquipedArrow { get => _equipedArrow; set => _equipedArrow = value; }
    public InventoryController InventoryController { get => _inventoryController; set => _inventoryController = value; }

    public void ToFly(bool fly)
    {
        _isflying = fly;
        OnplayerFlying?.Invoke(fly);
    }
}
