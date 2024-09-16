using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTile : MonoBehaviour
{
    public BaseItem item;
    public InventoryItemManager manager;

    [SerializeField]
    GameObject itemOptions;
    GameObject use;
    GameObject equip;
    GameObject unequip;
    GameObject trash;
    TextMeshProUGUI text;

    private void Awake()
    {
        use = itemOptions.transform.Find("Buttons").transform.Find("Use").gameObject;
        equip = itemOptions.transform.Find("Buttons").transform.Find("Equip").gameObject;
        unequip = itemOptions.transform.Find("Buttons").transform.Find("Unequip").gameObject;
        trash = itemOptions.transform.Find("Buttons").transform.Find("Trash").gameObject;
        text = itemOptions.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        itemOptions.SetActive(false);
    }

    public void OnClick()
    {
        if (itemOptions.activeSelf || item == null)
        {
            manager.DisableItemOptions();
            return;
        }

        manager.DisableItemOptions();
        itemOptions.SetActive(!itemOptions.activeSelf);

        use.SetActive(true);
        equip.SetActive(true);
        trash.SetActive(true);
        unequip.SetActive(false);

        if (item.Type != BaseItem.ItemTypes.POTABLES)
        {
            use.SetActive(false);
        }
        if (item.Type != BaseItem.ItemTypes.INSTRUMENTS)
        {
            equip.SetActive(false);
        }
        if (DataManager.Instance.PlayerData.Equipment.Contains(item))
        {
            trash.SetActive(false);
            equip.SetActive(false);
            unequip.SetActive(true);
        }

        text.text = $"{item.DisplayName}\n{item.Description}";
    }

    public void OnDeselect()
    {
        if (!itemOptions.activeSelf) return;
        itemOptions.SetActive(false);
    }

    public void Use()
    {
        // PotableItem.UseEffect
        DataManager.Instance.PlayerData.Inventory.Remove(item);
        manager.UpdateInventory();
    }

    public void Equip()
    {
        DataManager.Instance.PlayerData.Equipment.Add(item);
        manager.UpdateInventory();
    }

    public void Trash()
    {
        DataManager.Instance.PlayerData.Inventory.Remove(item);
        manager.UpdateInventory();
    }

    public void Unequip()
    {
        // unequip item effect here
        DataManager.Instance.PlayerData.Equipment.Remove(item);
        manager.UpdateInventory();
    }
}
