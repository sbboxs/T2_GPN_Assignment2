using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnchantEquipment : MonoBehaviour
{
    public TextMeshProUGUI enchantStatus;
    public static List<Equipment> equipmentList;
    public Equipment enchantEquipment;

    public GameObject enchantPanel;

    public void enchantItem()
    {
        enchantEquipment = EnchantSetUp.currentEquipment();

        Debug.Log(enchantEquipment.equipmentType);

        //Check if is max level
        if (enchantEquipment.equipmentEnchantLvl < 24)
        {
            //Check if enough gold
            //If()

            equipmentList = EnchantTrigger.GetEquipmentList();
            string selectedEquipmentType = enchantEquipment.equipmentType;
            if (selectedEquipmentType == "Weapon")
            {
                enchantEquipment.equipmentArritbute += 5;
            }
            else if (selectedEquipmentType == "Ring")
            {
                enchantEquipment.equipmentArritbute += 10;
            }
            else if (selectedEquipmentType == "Helmet")
            {
                enchantEquipment.equipmentArritbute += 10;
            }
            else if (selectedEquipmentType == "Armor")
            {
                enchantEquipment.equipmentArritbute += 5;
            }
            else
            {
                Debug.Log("Enchant type error");
            }
            enchantEquipment.equipmentEnchantLvl += 1;
            enchantEquipment.equipmentEnchantCost += 10 * enchantEquipment.equipmentEnchantLvl;

            foreach (Equipment equipment in equipmentList)
            {
                if (equipment.equipmentType == selectedEquipmentType)
                {
                    equipment.equipmentArritbute = enchantEquipment.equipmentArritbute;
                    equipment.equipmentEnchantCost = enchantEquipment.equipmentEnchantCost;
                    equipment.equipmentEnchantLvl = enchantEquipment.equipmentEnchantLvl;
                }
            }

            enchantStatus.text = "Enchance Successfuly!!";
            EnchantTrigger.updateEquipmentList();
            updateChracterAttribute(equipmentList,enchantEquipment);
            enchantPanel.SetActive(false);
            enchantPanel.SetActive(true);
        }
    }

    //Update Overall Character Attribute
    public static void updateChracterAttribute(List<Equipment> equipmentList, Equipment currentEquipment)
    {
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        foreach (Equipment equipment in equipmentList)
        {
            if (equipment.equipmentType == currentEquipment.equipmentType)
            {
                character.strength = equipment.equipmentArritbute;
            }
            else if (equipment.equipmentType == currentEquipment.equipmentType)
            {
                character.mana = equipment.equipmentArritbute;
            }
            else if (equipment.equipmentType == currentEquipment.equipmentType)
            {
                character.health = equipment.equipmentArritbute;
            }
            else
            {
                character.defense = equipment.equipmentArritbute;
            }
        }

        for (int i = 0; i < character.strengthStatsPt; i++)
        {
            character.strength += 5;
        }

        for (int i = 0; i < character.healthStatsPt; i++)
        {
            character.healthStatsPt += 10;
        }

        for (int i = 0; i < character.defenseStatsPt; i++)
        {
            character.defense += 5;
        }

        character.remainingStatsPt = character.level - character.healthStatsPt - character.strengthStatsPt - character.defenseStatsPt - 1;
        DataHandler.SaveToJSON(character, "CharacterAttribute");
    }
}

