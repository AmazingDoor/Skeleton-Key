using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleeperMove : BaseMonster
{
    int sight_range = 10;
    bool awake = false;
    
    // Start is called before the first frame update
    void Start()
    {
        initMonster();

    }

    // Update is called once per frame
    void Update()
    {

        if (!isAlive()) 
        {
            
            
            GetComponentInChildren<NavMeshAgent>().enabled = false;
            GetComponentInChildren<MeshCollider>().enabled = false;
            return; 
        }

        if (getHealth() <= 0) 
        {
            setAlive(false); 
        }

        if (awake)
        {
            if (canSeePlayer())
            {
                if (Vector3.Distance(transform.position, getPlayer().transform.position) > 2)
                {
                    setPlayerVisible(true);
                    chasePlayer();
                    setFocusTime(100);
                }
                else
                {
                    stopMoving();
                }
            }
            else
            {
                if(Vector3.Distance(gameObject.transform.position, getPlayer().transform.position) < .5)
                {
                    setFocusTime(0);
                }
                if (getFocusTime() > 0)
                {
                    setFocusTime(getFocusTime() - Time.deltaTime);
                }
                else if (getFocusTime() <= 0)
                {
                    agent.SetDestination(gameObject.transform.position);
                    awake = false;
                }
                setPlayerVisible(false);
            }
        
        }

        if (isVisible())
        {
            if (!getPlayer().GetComponent<PlayerLook>().monsters.Contains(gameObject)) {
                getPlayer().GetComponent<PlayerLook>().monsters.Add(gameObject);
            }
        }
        else 
        {
            getPlayer().GetComponent<PlayerLook>().monsters.Remove(gameObject);
        }
    }

    public override void onHear(int range)
    {
        awake = true;
        addFocusTime((float)range - Vector3.Distance(gameObject.transform.position, getPlayer().transform.position));
        setPositionGoal(getPlayer().transform);
    }

    bool canSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, getPlayer().transform.position, out hit))
        {
            if(hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    
}
