using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowHandler : MonoBehaviour
{
    public InventorySlot[] slots;
    public InventorySlot[] getSlots()
    {
        return slots;
    }

    public int getSize()
    {
        return slots.Length;
    }
}
