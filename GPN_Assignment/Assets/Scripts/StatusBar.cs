using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatusBar : MonoBehaviour
{
    public TextMeshProUGUI maxHealth;
    public TextMeshProUGUI maxMana;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI experience;

    void Start()
    {
        updateStatusBar();
    }
    private void OnEnable()
    {
        Debug.Log("Update status bar");
        updateStatusBar();
    }
    public void updateStatusBar()
    {
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        maxHealth.text = character.health.ToString() + " / " + character.health.ToString();
        maxMana.text = character.mana.ToString() + " / " + character.mana.ToString();
        gold.text = character.gold.ToString();
        experience.text = character.experience.ToString() + " / " + ((character.level + 1000) * 1.3).ToString();
    }
}
