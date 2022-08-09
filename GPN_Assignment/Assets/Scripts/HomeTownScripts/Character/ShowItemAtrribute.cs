using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShowItemAtrribute : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemFrame;
    public GameObject itemAttributeUI;

    public TextMeshProUGUI selectedItem;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI itemAttribute;
    public TextMeshProUGUI itemEnhanceLevel;

    public List<Equipment> equipmentList;
    public Equipment currentEquipment;
    private string equipmentType;

    void Start()
    {
        equipmentList = DataHandler.ReadListFromJSON<Equipment>("Equipment");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        equipmentType = selectedItem.text.ToString();

        foreach (Equipment equipment in equipmentList)
        {
            if(equipment.equipmentType == equipmentType)
            {
                currentEquipment = equipment;
                break;
            }
        }
        

        itemType.text = currentEquipment.equipmentType;
        if (equipmentType == "Weapon")
        {
            itemAttribute.text = currentEquipment.equipmentArritbute.ToString() + " Strength";
        }
        else if (equipmentType == "Ring")
        {
            itemAttribute.text = currentEquipment.equipmentArritbute.ToString() + " Mana";
        }
        else if (equipmentType == "Helmet")
        {
            itemAttribute.text = currentEquipment.equipmentArritbute.ToString() + " Health";
        }
        else if (equipmentType == "Armor")
        {
            itemAttribute.text = currentEquipment.equipmentArritbute.ToString() + " Defense";
        }
        else
        {
            Debug.Log(equipmentType + "Equipment type error");
        }

        itemEnhanceLevel.text = currentEquipment.equipmentEnchantLvl + " / 24";
        itemAttributeUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemAttributeUI.gameObject.SetActive(false);
    }

}
