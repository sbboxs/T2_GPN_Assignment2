using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUpScript : MonoBehaviour
{

    // Start is called before the first frame update
    public void setUpData()
    {
        //Setting up equipment stats
        List<Equipment> equipmentList = DataHandler.ReadListFromJSON<Equipment>("Equipment");
        CharacterAttribute character = new CharacterAttribute(1,1,100,100,1,0,0,0,0,0,0);
        if (equipmentList.Count <= 0)
        {
            Debug.Log("Setting up Equipment");

            //Strength
            Equipment newWeapon = new Equipment("Weapon", 1, 0,100);
            equipmentList.Add(newWeapon);

            //Mana
            Equipment newRing = new Equipment("Ring", 100, 0,100);
            equipmentList.Add(newRing);
        
            //Defense
            Equipment newArmor = new Equipment("Armor", 1, 0,100);
            equipmentList.Add(newArmor);

            //Health
            Equipment newHelmet = new Equipment("Helmet", 100, 0,100);
            equipmentList.Add(newHelmet);

            DataHandler.SaveToJSON(equipmentList, "Equipment");
        }

        //Setting up character attribute pt
        CharacterAttribute characterAttribute = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        if (characterAttribute == default(CharacterAttribute)){
            Debug.Log("Setting up Chracter Attributes");
            DataHandler.SaveToJSON(character, "CharacterAttribute");
        }
    }


    //重新计算最终属性
    public static void updateChracterAttribute()
    {
        List<Equipment> equipmentList = DataHandler.ReadListFromJSON<Equipment>("Equipment");
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");

        foreach (Equipment equipment in equipmentList)
        {
            if (equipment.equipmentType == "Weapon")
            {
                character.strength = equipment.equipmentArritbute;
            }
            else if (equipment.equipmentType == "Ring")
            {
                character.mana = equipment.equipmentArritbute;
            }
            else if (equipment.equipmentType == "Helmet")
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
