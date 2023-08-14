using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController player;
    float speed = 2.5f;
    float gravity = -10f;
    float jump_height = 5;
    bool in_inventory;
    bool hud_active;
    NoiseHandler noise_handler;

    public Vector3 move;
    public LayerMask ignore_layer;
    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;
    public bool is_grounded = false;
    public Canvas inventory;
    public Camera cam;
    public NewPlayerInventory inv;

    public Doge dog;

    //Scriptable Object Items
    //Prefabs go to DropHandler
    public Item revolver_bullet;
    public WeaponItem shot_gun;
    public WeaponItem revolver;
    public Item lock_pick;
    public Item shot_gun_shell;
    public Item lock_item;
    public Item crow_bar;

    public Canvas Hud;
    
    void Awake()
    {
        inventory.enabled = false;
    }


    void Start()
    {
        player = this.GetComponent<CharacterController>();
        noise_handler = GetComponent<NoiseHandler>();
        dog = GameObject.Find("Dog").GetComponent<Doge>();
        
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInInventory();
            toggleHud();
            

        }
        

        
        handleMovement();
        if (!in_inventory)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                } else
                {
                    Time.timeScale = 1;
                }
            }
            if(Time.timeScale == 0) { return; }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                Physics.Raycast(ray,out hit, 10, ~ignore_layer);
                Debug.Log(hit.transform.name);
                    if (hit.transform.tag == "bullet")
                    {
                        inv.addItem(revolver_bullet);
                    }
                    if(hit.transform.CompareTag("lock"))
                    {
                        inv.addItem(lock_item) ;
                    }
                    if(hit.transform.tag == "shot_gun")
                    {
                        inv.addItem(shot_gun);
                    }
                    if(hit.transform.tag == "revolver")
                    {
                        inv.addItem(revolver);
                    }
                    if (hit.transform.tag == "revolver_bullet")
                    {
                        inv.addItem(revolver_bullet);
                        Destroy(hit.transform.gameObject);
                    }
                    if(hit.transform.CompareTag("lock_pick"))
                    {
                        inv.addItem(lock_pick);
                        Destroy(hit.transform.gameObject);
                    }
                    if(hit.transform.CompareTag("shot_gun_shell"))
                    {
                        inv.addItem(shot_gun_shell);
                    }
                    if (hit.transform.CompareTag("dirt_pile"))
                    {
                    if (dog != null)
                    {
                        dog.setMoveGoal(hit.transform.gameObject);
                    }
                    }
                    if(hit.transform.CompareTag("crow_bar"))
                    {
                        inv.addItem(crow_bar);
                    }


                    if(hit.transform.CompareTag("door"))
                    {
                        if(hit.transform.GetComponent<Door>())
                        {
                            Door door = hit.transform.GetComponent<Door>();
                            door.toggleOpen();
                        }
                        
                    }
            }
        } 
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                toggleInInventory();
                toggleHud();
            }
        }
        
    }

    void handleMovement()
    {

        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");
        float speed_modifier;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed_modifier = 1.4f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed_modifier = 0.3f;
        }
        else
        {
            speed_modifier = 1;
        }
        if (move_x != 0 || move_z != 0)
        {
            if (speed_modifier > 1)
            {
                
                noise_handler.createSound(15, false);
            }
            else if (speed_modifier == 1)
            {
                noise_handler.createSound(6, false);
            }
            else
            {
                noise_handler.createSound(2, false);
            }
        }

        move = transform.right * move_x * speed_modifier + transform.up * move.y + transform.forward * move_z * speed_modifier;


        if (!in_inventory)
        {
            //if not paused
            is_grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            
            
            


            if (is_grounded && move.y < 0)
            {
                move.y = -2f;
            }


            if (Input.GetButtonDown("Jump") && is_grounded)
            {
                move.y = jump_height;
            }
            //end if not paused
        }
        

        move.y += gravity * Time.deltaTime;
        player.Move(new Vector3(move.x * speed, move.y, move.z * speed) * Time.deltaTime);
    }


    //inventory
    public void setInInventory(bool b)
    {
        this.in_inventory = b;
        inventory.enabled = b;
    }

    public void toggleInInventory()
    {
        if(in_inventory)
        {
            in_inventory = false;
            inventory.enabled = false;
        } else
        {
            in_inventory = true;
            inventory.enabled = true;
        }
    }

    public bool getInInventory()
    {
        return this.in_inventory;
    }
    
    //HUD

    public void setHud(bool b)
    {
        this.hud_active = b;
    }

    public void toggleHud()
    {
        if(this.hud_active == true)
        {
            hud_active = false;
        } else
        {
            hud_active = true;
        }
    }

    public bool getHud()
    {
        return hud_active;
    }



}
