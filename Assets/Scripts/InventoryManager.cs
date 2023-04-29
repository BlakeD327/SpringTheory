using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    // Static class saving all inventory item
    public static Dictionary<string, int> itemList = 
                        new Dictionary<string, int>();  // Map for item list

    public static void AddItem(GameObject o)
    {
        // Adding item to the list
        var name = o.name;
        itemList.TryAdd(name, 0);
        itemList[name]++;
    }

    // Use the item in the inventory
    public static void UseItem(GameObject o)
    {

    }

}
