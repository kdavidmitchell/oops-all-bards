using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftItemButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public BaseItem item;
    public InventoryItemManager manager;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayTooltip()
    {
        manager.UpdateCraftingTooltip(item);
    }
}
