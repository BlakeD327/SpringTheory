using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    public Image imgL;
    public Image imgM;
    public Image imgR;
    public TextMeshProUGUI textL;
    public TextMeshProUGUI textM;
    public TextMeshProUGUI textR;

    void Start()
    {
        inventory = Inventory.inventory;
        inventory.onItemChangedCallback += UpdateUI;
    }

    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");

        var itemList = inventory.DisplayItem();
        if(itemList == null)
        {
            EnableDisplay(false);
            return;
        }

        EnableDisplay();
        Debug.Log(itemList.Count);
        
        // Updating icon and count (left, middle, right)
        imgL.sprite = itemList[0].item.img;
        textL.text = $"{itemList[0].count}";

        imgM.sprite = itemList[1].item.img;
        textM.text = $"{itemList[1].count}";

        imgR.sprite = itemList[2].item.img;
        textR.text = $"{itemList[2].count}";
        
    }

    void EnableDisplay(bool enable = true)
    {
        imgL.enabled = enable;
        imgM.enabled = enable;
        imgR.enabled = enable;
        textL.enabled = enable;
        textM.enabled = enable;
        textR.enabled = enable;
    }
}
