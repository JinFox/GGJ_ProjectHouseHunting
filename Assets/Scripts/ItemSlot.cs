using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    Item _displayedItem = null;
    List<Item>  _availableItems;
    public void HideEverything()
    {
        // TODO: browse _availableItems and hide eveerything
        _displayedItem = null;
    }

    public Item DrawItemAndDisplay() {
        // Make sure everything is hidden first
        HideEverything();
        return _displayedItem;
        //TODO : draw a random item from the pool
    }

    public Item GetDisplayedItem()
    {
        return _displayedItem;
    }
}
