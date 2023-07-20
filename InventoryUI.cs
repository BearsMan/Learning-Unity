using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryScreen;
    public Inventory connectedInventory;
    public Transform content;
    public InventorySlot[] slots;
    public InventorySlot slotPrefab;
    public Item heldItem;
    public Image heldItemImage;

    private void Start()
    {
        SetupInventoryUI();
    }
    public void SetupInventoryUI()
    {
        connectedInventory.inventoryUpdated += UpdateInventoryScreen;
        if (slots.Length == 0)
        {
            slots = new InventorySlot[connectedInventory.backPack.Length];
            for (int i = 0; i < connectedInventory.backPack.Length; i++)
            {
                InventorySlot newSlot = Instantiate(slotPrefab, content);
                if (connectedInventory.backPack[i] != null)
                {
                    newSlot.itemImage.sprite = connectedInventory.backPack[i].itemImage;
                    
                }
                newSlot.slotClicked += SetHeldItem;
                slots[i] = newSlot;
            }
        }
    }
    public void UpdateInventoryScreen()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
            if (connectedInventory.backPack[i] != null)
            {
                slots[i].itemImage.sprite = connectedInventory.backPack[i].itemImage;
            }
            else
            {
                slots[i].itemImage.sprite = null;
            }
        }

        if (heldItem != null)
        {
            heldItemImage.enabled = true;
            heldItemImage.sprite = heldItem.itemImage;
        }
        else
        {
            heldItemImage.enabled = false;
        }
    }

    public void SetHeldItem(InventorySlot selectedSlot)
    {
        int index = System.Array.IndexOf(slots, selectedSlot);
        Debug.Log("Clicking slot " + index);
        Item previousHeldItem = heldItem;
        heldItem = connectedInventory.backPack[index];
        connectedInventory.backPack[index] = previousHeldItem;

        

        UpdateInventoryScreen();
    }

    public void ShowInventory()
    {
        inventoryScreen.SetActive(true);
        UpdateInventoryScreen();
    }
    
    public void HideInventory()
    {
        inventoryScreen?.SetActive(false);
        if (heldItem != null)
        {
            connectedInventory.AddToBackPack(heldItem);
            heldItem = null;
        }
        UpdateInventoryScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryScreen.activeSelf)
            {
                HideInventory();
            }

            else
            {
                ShowInventory();
            }
        }
    }
}
