using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropFromInventory : MonoBehaviour, IDropHandler
{
    GameObject player;
    public GameObject[] items;
    public NewPlayerInventory inv;
    string[] item_names = new string[] { "Revolver Bullet", "Lock Pick", "Shot Gun", "Revolver", "SawedOff", "Shotgun Shell" };
    public void OnDrop(PointerEventData eventData)
    {
        player = GameObject.Find("Player");
        GameObject dropped = eventData.pointerDrag;
        InventoryItem item = dropped.GetComponent<InventoryItem>();
        RaycastHit hit;
        Ray ray = new Ray(player.transform.position, -player.transform.up);
        if (Physics.Raycast(ray, out hit, 2))
        {
            int i = Array.FindIndex(item_names, s => s == item.item.name);
            
            for (int num = 0; num < item.count; num++)
            {
                Debug.Log(i);
                Instantiate(items[i], hit.point, transform.rotation);
            }

            inv.dropItem(item);
        }
    }

    private void Start()
    {
        
    }
}
