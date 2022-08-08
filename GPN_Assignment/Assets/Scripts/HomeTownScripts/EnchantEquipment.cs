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
                enchantEquipment.equipmentArritbute += 1;
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
                enchantEquipment.equipmentArritbute += 10;
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
            DataHandler.SaveToJSON(equipmentList, "Equipment");
            enchantStatus.text = "Enchance Successfuly!!";
            EnchantTrigger.updateEquipmentList();
            enchantPanel.SetActive(false);
            enchantPanel.SetActive(true);
        }
    }
}

