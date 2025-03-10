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
    int maxhealth;
    int currenthealth;
    int maxmana;
    int currentmana;
    double currentexp;
    double maxexp;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        updateStatusBar();
    }

    void Update()
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
        maxhealth = character.health;
        currenthealth = player.GetComponent<PlayerController>().currentHealth;
        maxmana = character.mana;
        currentmana = player.GetComponent<PlayerController>().currentMana;
        maxexp = (character.level + 1000) * 1.3;
        currentexp = player.GetComponent<PlayerController>().exp;
        maxHealth.text = currenthealth.ToString() + " / " + maxhealth.ToString();
        maxMana.text = currentmana.ToString() + " / " + maxmana.ToString();
        gold.text = character.gold.ToString();
        experience.text = currentexp.ToString() + " / " + maxexp.ToString();
    }
}
