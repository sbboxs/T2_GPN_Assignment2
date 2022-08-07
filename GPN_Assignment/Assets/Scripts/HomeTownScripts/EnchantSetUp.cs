using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnchantSetUp : MonoBehaviour
{
    public TextMeshProUGUI SelectedItemName;
    public TextMeshProUGUI EnchantItemName;

    public void setUpEnchant()
    {
        EnchantItemName.text = SelectedItemName.text;
    }
}
