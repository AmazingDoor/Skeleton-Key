using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Tool : ItemActionHandler
{
    public Item item;
    int max_unlock_time = 3;
    float unlock_time;
    public override void leftClickHoldAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler, NewPlayerInventory inventory)
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 2))
        {
            if(hit.transform.CompareTag("lock_on_door"))
            {
                Door door = hit.transform.GetComponentInParent<Door>();
                unlock_time -= 1 * Time.deltaTime;
                Debug.Log(unlock_time);
                if (unlock_time <= 0)
                {
                    door.Unlock();
                    inventory.removeItem(item);
                }
            }
        }
    }

    public override void cancelLeftClick()
    {
        unlock_time = max_unlock_time;
    }

    // Start is called before the first frame update
    void Start()
    {
        unlock_time = max_unlock_time;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
