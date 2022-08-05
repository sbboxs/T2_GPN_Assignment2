using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShowItemAtrribute : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemFrame;
    public GameObject itemAttributeUI;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAttributeName;
    private string currentItemName;
    void Start()
    {
        
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(currentItemName);
        itemAttributeName.text = itemName.text;
        itemAttributeUI.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemAttributeUI.gameObject.SetActive(false);
    }

}
