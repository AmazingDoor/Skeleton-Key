using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockItem : ItemActionHandler
{
    // Start is called before the first frame update
    public Item item;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void leftClickAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler, NewPlayerInventory inventory)
    {
        Debug.Log("Lock");
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit, 2)) {

            if (hit.transform.GetComponent<Door>())
            {
                Door door = hit.transform.GetComponent<Door>();
                if (!door.getOpen())
                {
                    door.Lock();
                    inventory.removeItem(item);
                }
             }
        }
    }
}
