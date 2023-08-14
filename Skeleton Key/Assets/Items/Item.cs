using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]

public class Item : ScriptableObject
{
    public Sprite sprite;

    public bool stackable = true;
    public bool large = false;
   
    public int x_offset = 0;

    public int noise;
    public bool loud;
    public ItemType item_type;

    [HideInInspector]
    public GameObject graphic;
    [HideInInspector]
    public ItemActionHandler actionHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum ItemType
{
    Default,
    Weapon,
    Lock,
    Tool,
    CrowBar
}
