using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] backPack = new Item[40];
    public int money = 10;
    public delegate void InventoryUpdated();
    public InventoryUpdated inventoryUpdated;

    private void Start()
    {
        string output = DataConversion.ItemsToString(backPack);
        Debug.Log(output);
    }

    public bool AddToBackPack(Item newItem)
    {
        for(int i = 0; i < backPack.Length; i++)
        {
            if (backPack[i] == null)
            {
                backPack[i] = newItem;
                inventoryUpdated?.Invoke();
                return true;
            }
        }
        
        return false;
    }

    public bool SetBackPackSlot(Item newItem, int slotNumber)
    {
        if (backPack[slotNumber] == null)
        {
            backPack[slotNumber] = newItem;
            inventoryUpdated?.Invoke();
            return true;
        }
        return false;
    }

    public Item RemoveItemFromSlot(int slotNumber)
    {
        Item slotItem = backPack[slotNumber];
        backPack[slotNumber] = null;
        inventoryUpdated?.Invoke();
        return slotItem;
    }

    public bool HasItem(Item itemToCheck)
    {
        return backPack.Contains(itemToCheck);
    }

    public bool RemoveItem(Item itemToRemove)
    {
        if (itemToRemove.questItem) return false;
        if (HasItem(itemToRemove))
        {
            int i = System.Array.IndexOf(backPack, itemToRemove);
            backPack[i] = null;
            inventoryUpdated?.Invoke();
            return true;
        }
        return false;
    }

    public bool RemoveQuestItem(Item itemToRemove)
    {
        
        if (HasItem(itemToRemove))
        {
            int i = System.Array.IndexOf(backPack, itemToRemove);
            backPack[i] = null;
            inventoryUpdated?.Invoke();
            return true;
        }
        return false;
    }
}
