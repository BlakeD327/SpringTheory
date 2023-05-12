using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int maxWeight = 100;
    public const int maxSlot = 20;
    
    // ========================================================================
    // For faster use of inventory
    public static Inventory inventory;
    public delegate void OnItemChanged();

    // ========================================================================
    // Data member of the container
    public List<Item> availableItem 
        = new List<Item>();             // Items player is able to select
    public Dictionary<int, int> itemCount 
        = new Dictionary<int, int>();   // Key: Item ID, Value: Item count
    public int weight 
        {get; private set;} = 0;        // The sum of the weight of items
    public int selectedIndex
        {get; private set;} = -1;       // Selected item's index
    public OnItemChanged onItemChangedCallback; // Used to trigger events

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

        // Set selected index to 0 when there's item
        if(availableItem.Count == 1)
            selectedIndex = 0;

        itemCount[item.ID]++;               // Increment count by 1
        weight += item.weight;              // Increase weight based on item

        Debug.Log($"The count of [{item.name}] is {itemCount[item.ID]}");
        Debug.Log($"Total weight: {weight}");
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    // Use the item in the inventory
    public bool UseItem(Item item, out int updatedItemCount)
    {
        if(itemCount.TryGetValue(item.ID, out int value) && value > 0)
        {
            // Update count and weight to match after using the item
            updatedItemCount = --itemCount[item.ID];
            weight -= item.weight;

            if(updatedItemCount == 0)                   
            {
                if(availableItem[selectedIndex] == item)    // Move selected index away from this item
                    selectedIndex--;
                
                availableItem.Remove(item);             // Remove item from the list if no longer available
            }
            
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            return true;
        }
        
        Debug.Log("The item is not available!");
        updatedItemCount = 0;
        return false;
    }


    // =======================================================================
    // Selected index functions
    // =======================================================================
    public void SelectLeft()
    {
        if(selectedIndex == 0)
            selectedIndex = availableItem.Count - 1;
        else
            selectedIndex--;
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void SelectRight()
    {
        if(selectedIndex == availableItem.Count - 1)
            selectedIndex = 0;
        else
            selectedIndex++;
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    // =======================================================================
    // Misc fuctions
    // =======================================================================
    public List<(Item item, int count)> DisplayItem()
    {
        // Return empty list of no item in inventory
        if(availableItem.Count == 0)
            return null;

        // Gathering items adjacent to selected index
        List<(Item item, int count)> rtnList = new List<(Item item, int count)>(); 
        int indexL = selectedIndex == 0 ? availableItem.Count - 1 : selectedIndex - 1;
        int indexR = selectedIndex == availableItem.Count - 1 ? 0 : selectedIndex + 1;
        
        // Getting item on left
        Item target = availableItem[indexL];
        rtnList.Add((target, itemCount[target.ID]));

        // Getting item selected
        target = availableItem[selectedIndex];
        rtnList.Add((target, itemCount[target.ID]));

        // Getting item on right
        target = availableItem[indexR];
        rtnList.Add((target, itemCount[target.ID]));

        return rtnList;
    }

}
