using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerMove : BaseMonster
{
 

    public LayerMask ground_mask;
    public LayerMask player_mask;
    public Vector3 walk_point;
    //public ScaredHandler handler;
    bool walkPointSet;
    public bool active = false;
    public float walkPointRange;
    public float sightRange = 60;
    public bool playerInSightRange;
    bool visible;

    
    

    Vector3 prev_target_pos;
    Vector3 screen_position;
    Vector3 MoveTargetPlaceHolder;
    Vector3 MoveTargetPlaceHolderScreenPoint;
    Plane[] cameraFrustum;
    RaycastHit player_ray_hit;
    



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        offset_forward = 1;
        offset_side = 2;
        offset_backwards = 4;
    }



    

    void followPlayer()
    {
        if (!isVisible() && Vector3.Distance(transform.position, player.position) > 2.5)
        {
            agent.SetDestination(MoveTargetPlaceHolder);
            transform.LookAt(MoveTargetPlaceHolder);
        } else
        {
            agent.SetDestination(transform.position);
        }
    }

    bool playerHasEyeSight()
    {
        RaycastHit player_check_one;
        RaycastHit player_check_two;
        RaycastHit player_check_three;
        RaycastHit player_check_four;
        RaycastHit player_check_five;
        RaycastHit player_check_six;
        RaycastHit player_check_seven;
        RaycastHit player_check_eight;
        Camera player_cam = player.GetComponentInChildren<Camera>();
        //Debug.DrawLine(transform.position + (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position);
       // Debug.DrawLine(transform.position - (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position);
        //Debug.DrawLine(transform.position + (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position);
       // Debug.DrawLine(transform.position - (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position);
        //Debug.DrawLine(transform.position + (transform.up * 3) + (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position);
        //Debug.DrawLine(transform.position + (transform.up * 3) - (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position);
       // Debug.DrawLine(transform.position + (transform.up * 3) + (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position);
       // Debug.DrawLine(transform.position + (transform.up * 3) - (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position);
        if (Physics.Linecast(transform.position + (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position, out player_check_one) &&
            Physics.Linecast(transform.position - (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position, out player_check_two) &&
            Physics.Linecast(transform.position + (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position, out player_check_three) &&
            Physics.Linecast(transform.position - (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position, out player_check_four) &&
            Physics.Linecast(transform.position + (transform.up * 3) + (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position, out player_check_five) &&
            Physics.Linecast(transform.position + (transform.up * 3) - (transform.right * (offset_side - 0.1f)) - (transform.forward * offset_backwards), player_cam.transform.position, out player_check_six) &&
            Physics.Linecast(transform.position + (transform.up * 3) + (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position, out player_check_seven) &&
            Physics.Linecast(transform.position + (transform.up * 3) - (transform.right * (offset_side - 0.1f)) + (transform.forward * offset_forward), player_cam.transform.position, out player_check_eight))
        {
            if(player_check_one.transform.tag == "Player" ||
                player_check_two.transform.tag == "Player" ||
                player_check_three.transform.tag == "Player" ||
                player_check_four.transform.tag == "Player" ||
                player_check_five.transform.tag == "Player" ||
                player_check_six.transform.tag == "Player" ||
                player_check_seven.transform.tag == "Player" ||
                player_check_eight.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }



    void followPlayerNoSight()
    {
        MoveTargetPlaceHolderScreenPoint = player.GetComponentInChildren<Camera>().WorldToViewportPoint(MoveTargetPlaceHolder);
        if (!isVisible())
        {
            agent.SetDestination(MoveTargetPlaceHolder);
            transform.LookAt(MoveTargetPlaceHolder);


        } 
        else
        {

            RaycastHit check_one;
            RaycastHit check_two;
            if(Physics.Linecast(transform.position + (transform.forward * offset_forward) + (transform.right * offset_side) , player.transform.position, out check_one) &&
                Physics.Linecast(transform.position + (transform.forward * offset_forward) - (transform.right * offset_side), player.transform.position, out check_two))
            {
                if(check_one.transform.tag != "Player" && check_two.transform.tag != "Player")
                {
                    agent.SetDestination(MoveTargetPlaceHolder);
                    transform.LookAt(MoveTargetPlaceHolder);
                } else
                {
                    agent.SetDestination(transform.position);
                }
            } else
            {
                agent.SetDestination(transform.position);
            }
        }
        

        if (Vector3.Distance(transform.position, MoveTargetPlaceHolder) < 2)
        {
            active = false;
            
        }

    }

    void activateMonster()
    {
        if(playerHasEyeSight()) {active = true;}
    }
    
    void Update()
    {
        //if visible
        if (isVisible())
        {
            if (!getPlayer().GetComponent<PlayerLook>().monsters.Contains(gameObject))
            {
                getPlayer().GetComponent<PlayerLook>().monsters.Add(gameObject);
            }




            //if (player.GetComponent<PlayerLook>().monsters.Contains(gameObject)) { handler.setScared(true); } else { handler.setScared(false); }
            //if player is looking
            screen_position = player.GetComponentInChildren<Camera>().WorldToViewportPoint(transform.gameObject.transform.position);
            Ray player_ray = new Ray(player.transform.position, player.GetComponentInChildren<Camera>().transform.forward);
            if ((screen_position.x > 0.45f && screen_position.x < 0.65f && screen_position.y > 0.1f && screen_position.y < 0.9f))
            {
                //if player is in room
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player_mask);
                if (playerInSightRange)
                {
                    //Activate Monster
                    activateMonster();
                }
            } else if(Physics.Raycast(player_ray, out player_ray_hit, 100))
            {
                if(player_ray_hit.transform.gameObject == transform.gameObject)
                {
                    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player_mask);
                    if (playerInSightRange)
                    {
                        activateMonster();
                    }
                }
            }
        } else
        {

            getPlayer().GetComponent<PlayerLook>().monsters.Remove(gameObject);

            
        }
        //if active
        if (active)
        {
            //if player is in room
            if (playerHasEyeSight()) 
            {
                
                //set target to player pos
                MoveTargetPlaceHolder = player.position;
                //chase
                followPlayer();
             }
                //if player not in room
             else
             {
                //chase player
                followPlayerNoSight();
             }
            
        }
    }

    public override void onHear(int range)
    {
        return;
    }
}
