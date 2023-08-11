using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider collider;
    bool open;
    public bool locked;

    public MeshRenderer graphics;
    public BoxCollider lock_collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lock()
    {
        Debug.Log("Locked");
        if (!open)
        {
            locked = true;
            graphics.enabled = true;
            lock_collider.enabled = true;
        }
    }

    public void Unlock()
    {
        locked = false;
        graphics.enabled = false;
        lock_collider.enabled = false;
    }

    public void toggleOpen()
    {
        if (!locked)
        {
            open = !open;
            collider.isTrigger = open;
        }
    }

    public bool getOpen()
    {
        return open;
    }

}
