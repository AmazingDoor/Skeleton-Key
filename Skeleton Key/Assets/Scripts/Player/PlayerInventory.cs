using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public InventorySlot[] slots;
    public GameObject inventoryItemPrefab;

    public bool hasItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot != null && item_in_slot.item == item)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool removeItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if(item_in_slot != null && item_in_slot.item == item)
            {
                item_in_slot.count--;
                item_in_slot.refreshCount();
                return true;
            }
        }
        return false;
    }
    public bool addItem(Item item)
    {
        //stacking
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if (item_in_slot != null && item_in_slot.item == item && item_in_slot.item.stackable == true)
            {
                item_in_slot.count++;
                item_in_slot.refreshCount();
                return true;
            }
        }

        //look for empty slot
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
            if(item_in_slot == null)
            {
                spawnNewItem(item, slot);
                return true;
            }
        }
        return false;

    }

    void spawnNewItem(Item item, InventorySlot slot)
    {
        GameObject new_item = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inv_item = new_item.GetComponent<InventoryItem>();
        inv_item.initItem(item);
    }
}
