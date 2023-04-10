using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum itemTypes
    {
        flute,
        harold,
        orange,
        plum,
        shrinkRay
    }

    [HideInInspector]
    public List<itemTypes> inventory;

    void Start()
    {
        inventory = new List<itemTypes>();
    }

    void Update()
    {
        
    }

    public void AddItem(itemTypes item)
    {
        inventory.Add(item);
    }
    public bool CheckItem(itemTypes item)
    {
        foreach (itemTypes invItem in inventory)
            if (invItem.Equals(item)) return true;
        return false;
    }
}