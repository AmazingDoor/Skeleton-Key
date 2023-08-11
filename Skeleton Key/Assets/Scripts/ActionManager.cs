using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionManager : MonoBehaviour
{
    public NewPlayerInventory inv;
    public HotbarHandler hotbar;
    public Item bullet;
    public Item ShotGunShell;
    public PlayerMove playerMove;
    public Camera cam;
    public BloomHandler bloom_handler;
    public GameObject test_cube;
    //0:Lock Pick, 1:Revolver Bullet 2:Shot Gun,
    public GameObject[] InventoryItemGraphics;


    NoiseHandler handler;

    float reload_time = 0;
    string[] penetratable = new string[] {"monster", "Player", "door", "lock"};
    bool aiming = false;


    public Animator animator;

    

    void Start()
    {
        handler = GetComponentInParent<NoiseHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale == 0) { return; }

        setActiveItemOnScreen();

        if(Input.GetMouseButtonDown(0))
        {
            if(playerMove.getInInventory()) { return; }
            if(hotbar.getActiveItem() == null) { return; }
            hotbar.getInvItem().GetComponent<ItemActionHandler>().leftClickAction(bloom_handler, cam, handler, inv);
            
        }

        if (Input.GetMouseButton(0))
        {
            if (playerMove.getInInventory()) { return; }
            if (hotbar.getActiveItem() == null) { return; }
            hotbar.getInvItem().GetComponent<ItemActionHandler>().leftClickHoldAction(bloom_handler, cam, handler, inv);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (playerMove.getInInventory()) { return; }
            if (hotbar.getActiveItem() == null) { return; }
            hotbar.getInvItem().GetComponent<ItemActionHandler>().cancelLeftClick();
        }


        if (Input.GetKeyDown(KeyCode.R) && reload_time < 1)
        {
            if (playerMove.getInInventory()) { return; }
            if (hotbar.getActiveItem() == null) { return; }
            hotbar.getInvItem().GetComponent<ItemActionHandler>().Reload(hotbar, inv);
        }


        if(Input.GetMouseButton(1))
        {

                aiming = true;
        } else
        {
            aiming = false;
        }
        

    }

    public void setActiveItemOnScreen()
    {
        Item item = hotbar.getActiveItem();
        if(item == null) 
        { 
            foreach(GameObject o in InventoryItemGraphics)
            {
                if (o.GetComponentInChildren<MeshRenderer>())
                {
                    o.GetComponentInChildren<MeshRenderer>().enabled = false;
                } else
                {
                    o.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                    o.GetComponent<Animator>().enabled = false;
                }
            }
            return; 
        }
        foreach(GameObject o in InventoryItemGraphics)
        {
            
            if(o.name == item.name)
            {
                if (o.GetComponentInChildren<MeshRenderer>())
                {
                    o.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
                else
                {
                    o.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    o.GetComponent<Animator>().enabled = true;
                }
            } else
            {
                if (o.GetComponentInChildren<MeshRenderer>())
                {
                    o.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
                else
                {
                    o.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                    o.GetComponent<Animator>().enabled = false;
                }
            }
        }
    }

    public bool getAiming()
    {
        return aiming;
    }
}
