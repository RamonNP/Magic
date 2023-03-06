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
    [SerializeField] private Weapon _weaponEquiped;
    [SerializeField] private List<Transform> _itensEquipmentSpritePlayer;


    [Header("UI Text Config")]
    [SerializeField] private TextMeshProUGUI _ATKText;
    [SerializeField] private TextMeshProUGUI _DefText;
    [SerializeField] private RectTransform _manaCurrentBar;
    [SerializeField] private RectTransform _lifeCurrentBar;

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
        CastMana(50);
        LoseLife(50);
    }

    private void InitializeAtributes()
    {
        attributes.Initialize();
    }
    private void Update()
    {
        _ATKText.text = "ATK:"+attributes.ATK.ToString();
        _DefText.text = "DEF:" + attributes.DEF.ToString();
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
    }

    private void EquipedItem(Item item)
    {
        if (item.TypeItem.Equals(TypeItem.Rune))
        {
            //Debug.Log("EquipedRune " + item.TypeItem + "-" + item.NameItem +"-"+item.PowerRuneItem.MagicLevel);
            _powerRuneListEquiped.Add(item.PowerRuneItem);
            attributes.AddRuneBonus(item.PowerRuneItem.MagicLevel);
        }        
        if(!item.TypeItem.Equals(TypeItem.Rune) && !item.TypeItem.Equals(TypeItem.None) && !item.TypeItem.Equals(TypeItem.Alls))
        {
            //Debug.Log("EquipItem - " + item.NameItem);
            attributes.EquipItem(item);
            if(item.TypeItem.Equals(TypeItem.Armor))
            {
                Sprite[] allSprites = Resources.LoadAll<Sprite>("ItensInventory/" + item.TypeItem + "/" + item.SpriteItem.ToString());
                _itensEquipmentSpritePlayer[0].GetComponent<SpriteRenderer>().sprite = allSprites[0];
                _itensEquipmentSpritePlayer[1].GetComponent<SpriteRenderer>().sprite = allSprites[1];
                _itensEquipmentSpritePlayer[2].GetComponent<SpriteRenderer>().sprite = allSprites[2];
                _itensEquipmentSpritePlayer[3].GetComponent<SpriteRenderer>().sprite = allSprites[3];
                _itensEquipmentSpritePlayer[4].GetComponent<SpriteRenderer>().sprite = allSprites[4];
            }

        }
    }    
    private void CastMana(float amount)
    {
        attributes.CastMana((int)amount);
        _manaCurrentBar.localScale = new Vector3((float)attributes.ManaCurrent / (float)attributes.ManaMax, 1, 1);
    }    
    
    private void LoseLife(float amount)
    {
        attributes.LoseLife((int)amount);
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
            if (item.TypeItem.Equals(TypeItem.Armor))
            {
                Sprite[] allSprites = Resources.LoadAll<Sprite>("ItensInventory/" + item.TypeItem + "/0");
                _itensEquipmentSpritePlayer[0].GetComponent<SpriteRenderer>().sprite = allSprites[0];
                _itensEquipmentSpritePlayer[1].GetComponent<SpriteRenderer>().sprite = allSprites[1];
                _itensEquipmentSpritePlayer[2].GetComponent<SpriteRenderer>().sprite = allSprites[2];
                _itensEquipmentSpritePlayer[3].GetComponent<SpriteRenderer>().sprite = allSprites[3];
                _itensEquipmentSpritePlayer[4].GetComponent<SpriteRenderer>().sprite = allSprites[4];
            }
        }
    }

    public bool Isflying { get => _isflying; }
    public Attributes Attributes { get => attributes; set => attributes = value; }

    public void ToFly(bool fly)
    {
        _isflying = fly;
        OnplayerFlying?.Invoke(fly);
    }
}
