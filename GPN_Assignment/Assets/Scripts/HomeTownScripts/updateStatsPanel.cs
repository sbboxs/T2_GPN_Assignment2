using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class updateStatsPanel : MonoBehaviour
{
    //statsPanel
    public TextMeshProUGUI healthPt;
    public TextMeshProUGUI strengthPt;
    public TextMeshProUGUI defensePt;
    public TextMeshProUGUI remainingPt;

    private void OnEnable()
    {
        CharacterAttribute character = DataHandler.ReadFromJSON<CharacterAttribute>("CharacterAttribute");
        character.remainingStatsPt = character.level - character.healthStatsPt - character.strengthStatsPt - character.defenseStatsPt;
        Debug.Log("Update Remaining " + character.remainingStatsPt);
        healthPt.text = character.healthStatsPt.ToString();
        strengthPt.text = character.strengthStatsPt.ToString();
        defensePt.text = character.defenseStatsPt.ToString();
        remainingPt.text = character.remainingStatsPt.ToString();
    }
}
