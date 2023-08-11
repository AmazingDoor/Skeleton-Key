using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable object/WeaponItem")]
public class WeaponItem : Item
{

    public int max_ammo;
    public float reload_time;
    public int shots;
    public float shot_distance;
    public float fire_delay_time;
    public float bloom;
    public Item ammo_type;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
