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
        character.remainingStatsPt = character.level - character.healthStatsPt - character.strengthStatsPt - character.defenseStatsPt;
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
            character.remainingStatsPt = character.level - character.healthStatsPt - character.strengthStatsPt - character.defenseStatsPt;
            DataHandler.SaveToJSON(character,"CharacterAttribute");
            statsPanel.SetActive(false);
            statsPanel.SetActive(true);
            Debug.Log("End Remaining " + character.remainingStatsPt);
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
        character.remainingStatsPt = character.level;
        DataHandler.SaveToJSON(character, "CharacterAttribute");
        statsPanel.SetActive(false);
        statsPanel.SetActive(true);
        Debug.Log("Resetted");
    }
}
