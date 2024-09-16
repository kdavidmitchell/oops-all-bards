using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{
    [SerializeField] private string name;
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private ItemTypes type;
    [SerializeField] private int value;
    [SerializeField] private Sprite icon;
    [SerializeField] private List<BaseItem> recipe;

    public enum ItemTypes
    {
        DUDS,
        INSTRUMENTS,
        POTABLES
    }

    public BaseItem(string name, string displayName, string description, ItemTypes type, int value, List<BaseItem> recipe)
    {
        this.name = name;
        this.displayName = displayName;
        this.description = description;
        this.type = type;
        this.value = value;
        this.icon = Resources.Load<Sprite>($"Items/{name}");
        this.recipe = recipe;
    }

    public BaseItem(BaseItem item)
    {
        this.name = item.name;
        this.displayName = item.displayName;
        this.description = item.description;
        this.type = item.type;
        this.value = item.value;
        this.icon = item.icon;
        this.recipe = item.recipe;
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public string DisplayName
    {
        get { return this.displayName; }
        set { this.displayName = value; }
    }

    public string Description
    {
        get { return this.description; }
        set { this.description = value; }
    }

    public ItemTypes Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    public int Value
    {
        get { return this.value; }
        set { this.value = value; }
    }

    public Sprite Icon
    {
        get { return this.icon; }
        set { this.icon = value; }
    }

    public List<BaseItem> Recipe
    {
        get { return this.recipe; }
        set { this.recipe = value; }
    }

    public static BaseItem GetItem(string id)
    {
        foreach (BaseItem item in ItemData.items)
        {
            if (id == item.name) return item;
        }
        Debug.Log("item not found");
        return null;
    }
}
