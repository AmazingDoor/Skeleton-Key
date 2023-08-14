using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBar : Tool
{
    // Start is called before the first frame update
    private void Start()
    {
        max_unlock_time = 3;
        unlock_time = max_unlock_time;
    }

    public override void leftClickHoldAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler, NewPlayerInventory inventory)
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.CompareTag("lock_on_door"))
            {
                Door door = hit.transform.GetComponentInParent<Door>();
                unlock_time -= 1 * Time.deltaTime;
                Debug.Log(unlock_time);
                if (unlock_time <= 0)
                {
                    door.Unlock();
                }
            }
        }
    }
}
