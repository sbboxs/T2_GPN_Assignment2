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
            Equipment newWeapon = new Equipment("Weapon", 5, 0,100);
            equipmentList.Add(newWeapon);

            //Mana
            Equipment newRing = new Equipment("Ring", 100, 0,100);
            equipmentList.Add(newRing);
        
            //Defense
            Equipment newArmor = new Equipment("Armor", 5, 0,100);
            equipmentList.Add(newArmor);

            //Health
            Equipment newHelmet = new Equipment("Helmet", 100, 0,100);
            equipmentList.Add(newHelmet);

            DataHandler.SaveToJSON(equipmentList, "Equipment");
        }

        //Setting up character attribute 
        CharacterAttribute character = new CharacterAttribute(5, 5, 100, 100, 1, 0, 0, 0, 0, 0, 0);
        CharacterAttribute characterAttribute = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        if (characterAttribute == default(CharacterAttribute)){
            Debug.Log("Setting up Chracter Attributes");
            DataHandler.SaveToJSON(character, "CharacterAttribute");
        }

        //Setting up quest
        List<Quest> questList = new List<Quest>();
        //First quest
        Quest warriorSkeletonQuest = new Quest("Warrior Skeleton Invasion!", "Slay 20 warrior skeleton",20,0,50,100,"Not Accepted");
        //Second quest
        Quest archerSkeletonQuest = new Quest("Archer Skeleton Invasion!", "Slay 10 archer skeleton", 10, 0, 50, 100, "Not Accepted");
        //Third quest
        Quest bossSkeletonQuest = new Quest("Isn't he the last boss?!", "Slay 3 boss skeleton", 3, 0, 150, 300, "Not Accepted");
        questList.Add(warriorSkeletonQuest);
        questList.Add(archerSkeletonQuest);
        questList.Add(bossSkeletonQuest);
        DataHandler.SaveToJSON(questList, "Quest");
    }
}
