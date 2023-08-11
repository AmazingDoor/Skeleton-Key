using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon : ItemActionHandler
{
    public ActionManager action_manager;
    
    public WeaponItem item;

    public int loaded;
    public int max_ammo;
    public int noise;
    public bool loud;
    public float max_reload_time;
    public int shots;
    public Item ammo_type;

    public bool reloading;

    float current_reload_time = 0;
    float offset_count = .1f;
    bool fire_delay;
    float max_shot_distance;
    float fire_delay_time;
    float bloom;

    Animator animator;
    private void Start()
    {
        loaded = item.max_ammo;
        max_ammo = item.max_ammo;
        max_reload_time = item.reload_time;
        shots = item.shots;
        max_shot_distance = item.shot_distance;
        fire_delay_time = item.fire_delay_time;
        ammo_type = item.ammo_type;
        noise = item.noise;
        loud = item.loud;
        bloom = item.bloom;
        action_manager = GameObject.Find("Player").GetComponentInChildren<ActionManager>();


        foreach(GameObject o in action_manager.InventoryItemGraphics)
        {
            if(o.name == item.name)
            {
                animator = o.GetComponent<Animator>();
            }
        }
    }

    private void Update()
    {
        if(Time.timeScale == 0) { return; }
        if(current_reload_time > 0)
        {
            current_reload_time -= 1 * Time.deltaTime;
        }


    }

    public override void Reload(HotbarHandler hotbar, NewPlayerInventory inv)
    {
        if(loaded == max_ammo)
        {
            if(reloading) { animator.SetTrigger("reload_end"); }
            reloading = false;
            return;
        }
        if(inv.hasItem(ammo_type))
        {
            if(!reloading) { animator.SetTrigger("reload_start"); }
            reloading = true;
            current_reload_time = max_reload_time;
            StartCoroutine(handleReload(hotbar, inv));
        } else
        {
            if(reloading) { animator.SetTrigger("reload_end"); }
            reloading = false;
        }
    }


    IEnumerator handleReload(HotbarHandler hotbar, NewPlayerInventory inv)
    {
        yield return new WaitUntil(() => ((Input.GetMouseButton(0) && Time.timeScale == 1) || (Input.GetMouseButton(1) && Time.timeScale == 1) || current_reload_time < 1));
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && hotbar.getActiveItem() != null)
        {
            inv.removeItem(ammo_type);
            loaded++;
            Reload(hotbar, inv);
        } else
        {
            current_reload_time = 0;
            if(reloading) { animator.SetTrigger("reload_end"); }
            reloading = false;
        }
    }

    public override void leftClickAction(BloomHandler bloom_handler, Camera cam, NoiseHandler handler)
    {
        
        if(loaded > 0 && fire_delay == false)
        {
            for (int s = 0; s < shots; s++)
            {
                float rand_x = Random.Range(-bloom_handler.getMax(), bloom_handler.getMax()) / 800;
                float rand_y = Random.Range(-bloom_handler.getMax(), bloom_handler.getMax()) / 800;
                float rand_z = Random.Range(-bloom_handler.getMax(), bloom_handler.getMax()) / 800;
                Vector3 spread = (new Vector3(rand_x, rand_y, rand_z) + cam.transform.forward).normalized;
                Ray ray = new Ray(cam.transform.position, spread);
                findNextHit(ray, spread, 0, max_shot_distance);
            }
            handler.createSound(noise, loud);
            loaded--;
            bloom_handler.setBloom(bloom);
            fire_delay = true;
            StartCoroutine(resetFireDelay(fire_delay_time));
        }
        
    }

    IEnumerator resetFireDelay(float time)
    {
        yield return new WaitForSeconds(time);
        fire_delay = false;
    }


    //returns true if can be shot through
    bool doLogic(RaycastHit hit)
    {
        //Debug.Log(hit.transform.name);
        switch (hit.transform.tag)
        {
            case "monster":
                if (hit.transform.GetComponent<StalkerMove>()) { return false; }
                //kill monster
                if (hit.transform.gameObject.name == "Head")
                {
                    hit.transform.GetComponentInParent<Transform>().GetComponentInParent<BaseMonster>().headShot();
                }
                else
                {
                    hit.transform.GetComponentInParent<BaseMonster>().hurt();
                }
                return false;
            case "Player":
                return true;
            case "door":
                return true;
            case "lock_on_door":
                //break lock
                Door door = hit.transform.GetComponentInParent<Door>();
                door.Unlock();
                return true;
            default:
                return false;
        }
    }

    void findNextHit(Ray ray, Vector3 dir, float dist, float max_distance)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, max_distance - dist))
        {
            if (doLogic(hit) == true)
            {

                //Instantiate(test_cube, hit.point, Quaternion.identity);
                Ray new_ray = new Ray(new Vector3(hit.point.x + (offset_count * dir.x), hit.point.y + (offset_count * dir.y), hit.point.z + (offset_count * dir.z)), dir);
                findNextHit(new_ray, dir, hit.distance, max_distance);
            }
        }
    }
}
