using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemActionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void leftClickAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler, NewPlayerInventory inventory)
    {
        leftClickAction(bloom_handler, cam, handler);
        return;
    }
    public virtual void leftClickAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler)
    {
        return;
    }
    public virtual void leftClickAction()
    {
        return;
    }

    public virtual void Reload(HotbarHandler hotbar, NewPlayerInventory inv)
    {
        return;
    }
}
