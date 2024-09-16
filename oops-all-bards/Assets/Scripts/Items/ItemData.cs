using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    // went with the laziest implementation for now :P Can easily make a list of items in a JSON file to be read
    // into the item data class in the future if needed
    public static BaseItem stick = new BaseItem("stick", "Stick", "It's a stick.", BaseItem.ItemTypes.DUDS, 0, null);
    public static BaseItem greenHerb = new BaseItem("greenherb", "Green Herb", "A medicinal plant to treat battle wounds. Restores 10 HP.", BaseItem.ItemTypes.POTABLES, 5, null);
    public static BaseItem biggerStick = new BaseItem("biggerstick", "Bigger Stick", "It's a bigger, scarier stick.", BaseItem.ItemTypes.INSTRUMENTS, 10, new List<BaseItem>() { stick, stick });

    public static BaseItem[] items = { stick, greenHerb, biggerStick };

}
