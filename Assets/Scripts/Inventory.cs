using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum itemTypes
    {
        nothing,
        flute,
        harold,
        plum,
        shrinkRay,
        hat,
        hatInfo,
        tinySydney,
        harold2Info,
        samEaten
    }

    List<itemTypes> inventory;

    void Start()
    {
        inventory = new List<itemTypes>();
    }

    public void AddItem(itemTypes item)
    {
        if (item == itemTypes.nothing) return;
        inventory.Add(item);
    }
    public bool CheckItem(itemTypes item)
    {
        foreach (itemTypes invItem in inventory)
            if (invItem.Equals(item)) return true;
        return false;
    }
    public void RemoveItem(itemTypes item)
    {
        inventory.Remove(item);
    }
}