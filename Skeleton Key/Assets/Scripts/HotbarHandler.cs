using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarHandler : MonoBehaviour
{
    RowHandler[] rows;
    InventorySlot[] slots;
    int highlighted_slot = 0;
    int last_scroll_val = 0;
    ActionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rows = GetComponentsInChildren<RowHandler>();
        slots = rows[0].getSlots();
        manager = GameObject.Find("Player").GetComponentInChildren<ActionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetAxis("Mouse ScrollWheel") > last_scroll_val)
        {
            if(highlighted_slot == slots.Length - 1)
            {
                highlighted_slot = 0;
            }
            else if (slots[highlighted_slot + 1].isInUse())
            {
                highlighted_slot += 2;
            } else
            {
                highlighted_slot++;
            }
            if(highlighted_slot > 2)
            {
                highlighted_slot = 0;
            } 
            last_scroll_val = (int)Input.GetAxis("Mouse ScrollWheel");
        } else if(Input.GetAxis("Mouse ScrollWheel") < last_scroll_val)
        {
            if(highlighted_slot == 0)
            {
                if (!slots[slots.Length - 1].isInUse())
                {
                    highlighted_slot = slots.Length - 1;
                } else
                {
                    highlighted_slot = slots.Length - 2;
                }
            }
            else if (slots[highlighted_slot - 1].isInUse())
            {
                highlighted_slot -= 2;
            }
            else
            {
                highlighted_slot--;
            }
            if(highlighted_slot < 0)
            {
                highlighted_slot = slots.Length - 1;
            }
            last_scroll_val = (int)Input.GetAxis("Mouse ScrollWheel");
            
        }
        //Debug.Log(slots[highlighted_slot].GetComponentInChildren<InventoryItem>().item.name);
    }

    public Item getActiveItem()
    {
        if (slots[highlighted_slot].transform.childCount > 0) {
            return slots[highlighted_slot].GetComponentInChildren<InventoryItem>().item;

        } else
        {
            return null;
        }
    }

    public InventoryItem getInvItem()
    {
        if (slots[highlighted_slot].transform.childCount > 0)
        {
            return slots[highlighted_slot].GetComponentInChildren<InventoryItem>();

        }
        else
        {
            return null;
        }
    }
}
