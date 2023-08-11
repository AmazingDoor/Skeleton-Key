using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool inUse;

    int index;
    ActionManager manager;

    public InventorySlot[] slots;

    private void Start()
    {
        slots = GetComponentInParent<RowHandler>().getSlots();
        index = Array.IndexOf(slots, this);
        manager = GameObject.Find("Player").GetComponentInChildren<ActionManager>();
    }

    private void Update()
    {
        if (index > 0)
        {
            if (slots[index - 1].transform.childCount != 0)
            {
                if (slots[index - 1].transform.GetChild(0).gameObject.GetComponent<InventoryItem>().item.large)
                {
                    inUse = true;
                } else
                {
                    inUse = false;
                } 
            } else
            {
                inUse = false;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem item = dropped.GetComponent<InventoryItem>();

        
        if(item.item.large)
        {
            
            if (transform.childCount == 0 && !inUse && index < slots.Length - 1 && slots[index + 1].transform.childCount == 0)
            {
                Debug.Log("large");
                item.parentAfterDrag = transform;
            }
        } 
        else if (transform.childCount == 0 && !inUse)
        {
            Debug.Log("small");
            item.parentAfterDrag = transform;
        } else
        {
            Debug.Log("nope");
        }
        manager.setActiveItemOnScreen();
    }

    public void setInUse(bool b)
    {
        inUse = b;
    }

    public bool isInUse()
    {
        return inUse;
    }
}
