using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftTooltip : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public BaseItem item;
    public InventoryItemManager manager;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        image = transform.Find("Image").GetComponent<Image>(); ;
    }

    public void CraftItem()
    {
        foreach(BaseItem item in item.Recipe)
        {
            foreach (BaseItem inventoryItem in DataManager.Instance.PlayerData.Inventory)
            {
                if (BaseItem.GetItem(inventoryItem.Name) == item)
                {
                    DataManager.Instance.PlayerData.Inventory.Remove(inventoryItem);
                    break;
                }
            }
        }
        DataManager.Instance.PlayerData.Inventory.Add(new BaseItem(this.item));
        manager.UpdateCraftingTab();
        manager.recipeTooltip.SetActive(false);
    }
}
