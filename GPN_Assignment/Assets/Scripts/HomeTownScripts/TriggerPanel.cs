using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPanel : MonoBehaviour
{
    public GameObject detailPanel;
    public bool isDisplayed = true;

    public void ButtonClicked()
    {
        detailPanel.SetActive(isDisplayed);
    }
}
