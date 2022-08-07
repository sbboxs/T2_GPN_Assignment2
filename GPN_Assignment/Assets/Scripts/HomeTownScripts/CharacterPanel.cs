using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterPanel : MonoBehaviour
{
    public GameObject attributePanel;
    public GameObject statsPanel;

    //attributePanel
    public TextMeshProUGUI level;
    public TextMeshProUGUI health;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI defense;

    public CharacterAttribute character;

    // Start is called before the first frame update

    public void showAttributePanel()
    {
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        attributePanel.SetActive(true);
        statsPanel.SetActive(false);
        level.text = character.level.ToString();
        health.text = character.health.ToString();
        strength.text = character.strength.ToString();
        defense.text = character.defense.ToString();

    }

    public void showStatsPanel()
    {
        statsPanel.SetActive(true);
        attributePanel.SetActive(false);
    }
}
