using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditStats : MonoBehaviour
{
    public TextMeshProUGUI selectedStats;
    public CharacterAttribute character;
    public GameObject statsPanel;

    public void addStats()
    {
        character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        string currentStats = selectedStats.text.ToString();
        Debug.Log("chosenStats: " + currentStats);

        if (character.remainingStatsPt > 0)
        {
            if (currentStats == "Health")
            {
                character.healthStatsPt += 1;
                Debug.Log("Health Added");
            }
            else if (currentStats == "Strength")
            {
                character.strengthStatsPt += 1;
                Debug.Log("Strength Added");
            }
            else if (currentStats == "Defense")
            {
                character.defenseStatsPt += 1;
                Debug.Log("Defense Added");
            }
            else
            {
                Debug.Log(currentStats + " Invalid stats");
            }

            character.remainingStatsPt -= 1;
            //update最终属性
            updateChracterAttribute(character);
            statsPanel.SetActive(false);
            statsPanel.SetActive(true);
            Debug.Log("Added Stat");
        }
        else
        {
            Debug.Log("Not enuf stat pt");
        }

    }

    public void resetStats()
    {
        character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        character.healthStatsPt = 0;
        character.strengthStatsPt = 0;
        character.defenseStatsPt = 0;
        character.remainingStatsPt = character.level - 1;
        updateChracterAttribute(character);
        statsPanel.SetActive(false);
        statsPanel.SetActive(true);
        Debug.Log("Resetted");
    }

    //Update Overall Character Attribute
    public static void updateChracterAttribute(CharacterAttribute character)
    {
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
