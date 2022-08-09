using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageDialogueTrioger : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Start()
    {
        stageDialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
