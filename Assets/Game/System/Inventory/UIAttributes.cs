using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAttributes : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaMaxValue;
    [SerializeField] private TextMeshProUGUI lifeMaxValue;
    [SerializeField] private TextMeshProUGUI magicPowerValue;
    [SerializeField] private TextMeshProUGUI physicalPowerValue;
    [SerializeField] private TextMeshProUGUI distanceFightValue;
    [SerializeField] private TextMeshProUGUI shieldValue;
    [SerializeField] private TextMeshProUGUI bonusEquipmentMagicPowerValue;
    [SerializeField] private TextMeshProUGUI bonusEquipmentPhysicalPowerValue;
    [SerializeField] private TextMeshProUGUI bonusEquipmentDistanceFightValue;
    [SerializeField] private TextMeshProUGUI bonusEquipmentShieldValue;
    [SerializeField] private TextMeshProUGUI bufferArmorSETValue;
    [SerializeField] private TextMeshProUGUI bufferItemWeapomLabelValue;    
    [SerializeField] private int intManaMaxValue;
    [SerializeField] private int intLifeMaxValue;
    [SerializeField] private int intMagicPowerValue;
    [SerializeField] private int intPhysicalPowerValue;
    [SerializeField] private int intDistanceFightValue;
    [SerializeField] private int intShieldValue;
    [SerializeField] private int intBonusEquipmentMagicPowerValue;
    [SerializeField] private int intBonusEquipmentPhysicalPowerValue;
    [SerializeField] private int intBonusEquipmentDistanceFightValue;
    [SerializeField] private int intBonusEquipmentShieldValue;
    [SerializeField] private int intBufferArmorSETValue;
    [SerializeField] private int intBufferItemWeapomLabelValue;
    [SerializeField] private GameObject _lifeInfoObject;
    [SerializeField] private GameObject _manaInfoObject;
    [SerializeField] private GameObject _physicalPowerInfoObject;
    [SerializeField] private GameObject _magicPowerInfoObject;
    [SerializeField] private GameObject _defenseInfoObject;
    [SerializeField] private GameObject _distanceFightInfoObject;



    public void UpdateStatus(Attributes attribute)
    {
        UpdateAttributeValue(attribute.ManaMax, ref manaMaxValue, ref intManaMaxValue);
        UpdateAttributeValue(attribute.LifeMax, ref lifeMaxValue, ref intLifeMaxValue);
        UpdateAttributeValue(attribute.AtkMagicPower, ref magicPowerValue, ref intMagicPowerValue);
        UpdateAttributeValue(attribute.AtkPhysicalPower, ref physicalPowerValue, ref intPhysicalPowerValue);
        UpdateAttributeValue(attribute.AtkDistanceFight,ref  distanceFightValue, ref intDistanceFightValue);
        UpdateAttributeValue(attribute.DEF, ref shieldValue, ref intShieldValue);
        UpdateAttributeValue(attribute.BonusEquipmentMagicPower,ref bonusEquipmentMagicPowerValue, ref intBonusEquipmentMagicPowerValue);
        UpdateAttributeValue(attribute.BonusEquipmentPhysicalPower, ref bonusEquipmentPhysicalPowerValue, ref intBonusEquipmentPhysicalPowerValue);
        UpdateAttributeValue(attribute.BonusEquipmentDistanceFight, ref bonusEquipmentDistanceFightValue, ref intBonusEquipmentDistanceFightValue);
        UpdateAttributeValue(attribute.BonusEquipmentDEF, ref bonusEquipmentShieldValue, ref intBonusEquipmentShieldValue);
        UpdateAttributeValue(attribute.BufferItemArmor, ref bufferItemWeapomLabelValue, ref intBufferItemWeapomLabelValue);
     }
    public void UpdateAttributeValue(int attributeValue, ref TextMeshProUGUI attributeText, ref int intValue)
    {
        var sign = intValue < attributeValue ? "+" : "-";
        var color = sign == "+" ? "<color=green>" : "<color=red>";
        if(intValue == attributeValue)
        {
            attributeText.text = $"{attributeValue}";
        } else
        {
            attributeText.text = $"{attributeValue} ({sign}{color}{Mathf.Abs(attributeValue - intValue)}</color>)";
        }
        intValue = attributeValue;
    }

    public TextMeshProUGUI ManaMaxValue { get => manaMaxValue; set => manaMaxValue = value; }
    public TextMeshProUGUI LifeMaxValue { get => lifeMaxValue; set => lifeMaxValue = value; }
    public TextMeshProUGUI MagicPowerValue { get => magicPowerValue; set => magicPowerValue = value; }
    public TextMeshProUGUI PhysicalPowerValue { get => physicalPowerValue; set => physicalPowerValue = value; }
    public TextMeshProUGUI DistanceFightValue { get => distanceFightValue; set => distanceFightValue = value; }
    public TextMeshProUGUI ShieldValue { get => shieldValue; set => shieldValue = value; }
    public TextMeshProUGUI BonusEquipmentMagicPowerValue { get => bonusEquipmentMagicPowerValue; set => bonusEquipmentMagicPowerValue = value; }

    public TextMeshProUGUI BonusEquipmentPhysicalPowerValue
    {
        get { return bonusEquipmentPhysicalPowerValue; }
        set { bonusEquipmentPhysicalPowerValue = value; }
    }


    public TextMeshProUGUI BonusEquipmentDistanceFightValue
    {
        get { return bonusEquipmentDistanceFightValue; }
        set { bonusEquipmentDistanceFightValue = value; }
    }


    public TextMeshProUGUI BonusEquipmentShieldValue
    {
        get { return bonusEquipmentShieldValue; }
        set { bonusEquipmentShieldValue = value; }
    }

    public TextMeshProUGUI BufferArmorSETValue
    {
        get { return bufferArmorSETValue; }
        set { bufferArmorSETValue = value; }
    }

    public TextMeshProUGUI BufferItemWeapomLabelValue
    {
        get { return bufferItemWeapomLabelValue; }
        set { bufferItemWeapomLabelValue = value; }
    }

    public void UpdateItemInfoText(GameObject button)
    {
        Item item = button.transform.GetComponent<ItemSlot>().Item;
        UpdateInfoText(_lifeInfoObject, "Life", item.EquipmentStatus.Life);
        UpdateInfoText(_manaInfoObject, "Mana", item.EquipmentStatus.Mana);
        UpdateInfoText(_physicalPowerInfoObject, "Physical Power", item.EquipmentStatus.PhysicalPower);
        UpdateInfoText(_magicPowerInfoObject, "Magic Power", item.EquipmentStatus.MagicPower);
        UpdateInfoText(_defenseInfoObject, "Defense", item.EquipmentStatus.Defense);
        UpdateInfoText(_distanceFightInfoObject, "Distance Fight", item.EquipmentStatus.DistanceFight);
    }

    private void UpdateInfoText(GameObject infoObject, string label, int value)
    {
        Debug.Log(label + value);
        if (value == 0)
        {
            infoObject.SetActive(false);
            return;
        }
        infoObject.SetActive(true);
        // Pega o texto do label e valor dos filhos do objeto
        TextMeshProUGUI labelObject = infoObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI valueObject = infoObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Atualiza os textos
        labelObject.text = label;
        valueObject.text = value.ToString();
    }

}
