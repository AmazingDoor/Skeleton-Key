using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerInventory : MonoBehaviour
{
    RowHandler[] rows;
    InventorySlot[] slots;
    public RowHandler[] hotBar;
    public GameObject inventoryItemPrefab;

    private void Start()
    {
        RowHandler[] temp_rows = GetComponentsInChildren<RowHandler>();
        rows = new RowHandler[temp_rows.Length + 1];
        for(int i = 0; i < rows.Length - 1; i++) 
        {
            rows[i] = temp_rows[i];
        }
        rows[temp_rows.Length] = hotBar[0];
    }

    public void dropItem(InventoryItem item)
    {

        //InventoryItem item = GameObject.Find("Inventory/Item(Clone)").GetComponent<InventoryItem>();
        item.count = 0;
        item.refreshCount();
    }

    public bool hasItem(Item item)
    {
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
                if (item_in_slot != null && item_in_slot.item == item)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool removeItem(Item item)
    {
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();
                if (item_in_slot != null && item_in_slot.item == item)
                {
                    item_in_slot.count--;
                    item_in_slot.refreshCount();
                    return true;
                }
            }
        }
        return false;
    }
    public bool addItem(Item item)
    {
        //stacking
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
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
        }

        //look for empty slot
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();

                if (item.large)
                {
                    if (i < slots.Length - 1) {
                        InventorySlot second_slot = slots[i + 1];
                        InventoryItem item_in_slot_2 = second_slot.GetComponentInChildren<InventoryItem>();
                        if ((item_in_slot == null && !slot.isInUse()) && (item_in_slot_2 == null && !second_slot.isInUse()))
                        {
                            spawnNewItem(item, slot);
                            return true;
                        }
                    }
                }
                else
                {
                    
                    if (item_in_slot == null && !slot.isInUse())
                    {
                        spawnNewItem(item, slot);
                        return true;
                    }
                }
            }
        }
        return false;

    }


    public bool addItem(WeaponItem item)
    {
        //stacking
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
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
        }

        //look for empty slot
        for (int a = 0; a < rows.Length; a++)
        {
            slots = rows[a].getSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem item_in_slot = slot.GetComponentInChildren<InventoryItem>();

                if (item.large)
                {
                    if (i < slots.Length - 1)
                    {
                        InventorySlot second_slot = slots[i + 1];
                        InventoryItem item_in_slot_2 = second_slot.GetComponentInChildren<InventoryItem>();
                        if ((item_in_slot == null && !slot.isInUse()) && (item_in_slot_2 == null && !second_slot.isInUse()))
                        {
                            spawnNewItem(item, slot);
                            return true;
                        }
                    }
                }
                else
                {

                    if (item_in_slot == null && !slot.isInUse())
                    {
                        spawnNewItem(item, slot);
                        return true;
                    }
                }
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

    void spawnNewItem(WeaponItem item, InventorySlot slot)
    {
        GameObject new_item = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inv_item = new_item.GetComponent<InventoryItem>();
        inv_item.initItem(item);
    }
}
