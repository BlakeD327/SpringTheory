using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int maxWeight = 100;
    public const int maxSlot = 20;
    
    // ========================================================================
    // For faster use of inventory
    public static Inventory inventory;
    public static Add addItem;
    public static Use useItem;

    // ========================================================================
    // Data member of the container
    public List<Item> availableItem 
        = new List<Item>();             // Items player is able to select
    public Dictionary<int, int> itemCount 
        = new Dictionary<int, int>();   // Key: Item ID, Value: Item count
    public int weight 
        {get; private set;} = 0;        // The sum of the weight of items

    // ========================================================================
    // Shorter syntax available for other class
    public delegate bool Add(Item item);
    public delegate bool Use(Item item, out int updatedItemCount);

    // ========================================================================
    // Initialize a static copy of the class
    void Awake()
    {
        if(inventory != null)
        {
            Debug.LogError("More than one inventory object created");
            return;
        }
        
        inventory = this;
        addItem = new Add(this.AddItem);
        useItem = new Use(this.UseItem);
    }

    public bool AddItem(Item item)
    {
        // Case: New weight exceed maxWeight
        if(item.weight > maxWeight - weight)
        {
            Debug.Log("Too heavy to pickup!");
            return false;
        }

        // Case: Slot is full and item is not already picked up
        if(availableItem.Count == maxSlot && !availableItem.Contains(item))
        {
            Debug.Log("The slots are full!");
            return false;
        }

        // Add item to available to makes it accessible by player
        if(itemCount.TryAdd(item.ID, 0) || itemCount[item.ID] == 0)
            availableItem.Add(item);

        itemCount[item.ID]++;               // Increment count by 1
        weight += item.weight;              // Increase weight based on item

        Debug.Log($"The count of [{item.name}] is {itemCount[item.ID]}");
        Debug.Log($"Total weight: {weight}");

        return true;
    }

    // Use the item in the inventory
    public bool UseItem(Item item, out int updatedItemCount)
    {
        if(itemCount.TryGetValue(item.ID, out int value) && value > 0)
        {
            updatedItemCount = --itemCount[item.ID];    // Decrement item count and retrieve it
            weight -= item.weight;                      // Decrease the weight by the item removed

            if(updatedItemCount == 0)                   // Remove item from the list if no longer available
                availableItem.Remove(item);
            
            return true;
        }
        
        Debug.Log("The item is not available!");
        updatedItemCount = 0;
        return false;
    }
}
