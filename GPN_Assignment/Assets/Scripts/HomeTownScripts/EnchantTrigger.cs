using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantTrigger : MonoBehaviour
{
    [Header("Enchant Visual Cue")]
    [SerializeField] private GameObject enchantVisualCue;

    [Header("Enchant Panel")]
    [SerializeField] private GameObject enchantPanel;

    public GameObject playButton;
    public GameObject characterButton;
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        enchantVisualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            enchantVisualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I))
            {
                playButton.SetActive(false);
                characterButton.SetActive(false);
                enchantPanel.SetActive(true);
            }
        }
        else
        {
            enchantVisualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
