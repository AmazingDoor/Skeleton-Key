using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider collider;
    bool open;
    public bool locked;
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
        }
    }

    public void Unlock()
    {
        locked = false;
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
