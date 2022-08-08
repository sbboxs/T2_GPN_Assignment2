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
            characterAttribute = new CharacterAttribute(1,1,100,1,10000,0, 0, 0, 0);

            DataHandler.SaveToJSON(characterAttribute, "CharacterAttribute");
        }
    }

    public static void updateChracterAttribute()
    {
        DataHandler.ReadFromJSON(
    }

    
}
