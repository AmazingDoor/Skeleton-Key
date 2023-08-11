using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseMonster : MonoBehaviour
{
    public float focus_time;
    bool player_visible;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Transform player;
    Plane[] cameraFrustum;
    [HideInInspector]
    public ScaredHandler handler;
    bool alive = true;
    int health = 2;
    [HideInInspector]
    public int offset_side = 1;
    [HideInInspector]
    public int offset_forward = 1;
    [HideInInspector]
    public int offset_backwards = 1;
    [HideInInspector]
    public int height = 2;

    

    
    public void initMonster()
    {
        agent = GetComponent<NavMeshAgent>();
        handler = GameObject.Find("ScaredPostProcessing").GetComponent<ScaredHandler>();

    }
    public ScaredHandler getScaredHandler()
    {
        return handler;
        
    }

    public bool isVisible()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(getPlayer().transform.GetComponentInChildren<Camera>());
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, gameObject.GetComponentInChildren<MeshCollider>().bounds))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public void setFocusTime(float time)
    {
        focus_time = time;
    }
    public void addFocusTime(float time)
    {
        focus_time += time;
    }

    public float getFocusTime()
    {
        return focus_time;
    }

    public void setPlayerVisible(bool visible)
    {
        player_visible = visible;
    }

    public bool playerVisible()
    {
        return player_visible;
    }

    public void stopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public void wander(int range)
    {

        Vector3 new_location = new Vector3(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range), transform.position.z + Random.Range(-range, range));
        agent.SetDestination(new_location);
        transform.LookAt(new_location);
    }

    public void chasePlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        agent.SetDestination(player.transform.position);
        transform.LookAt(getPlayer().transform);
    }

    public GameObject getPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public bool isAlive()
    {
        return alive;
    }

    public void setAlive(bool b)
    {
        alive = b;
        agent.SetDestination(transform.position);
    }

    public void setHealth(int h)
    {
        health = h;
    }
    public int getHealth()
    {
        return health;
    }
    public void hurt()
    {
        if(health > 0)
        {
            health--;
        }
    }
    public void headShot()
    {
        if (health >= 1)
        {
            health -= 2;
        }
    }
    public void setPositionGoal(Transform t)
    {
        if(alive)
        {
            agent.SetDestination(t.position);
        }
    }

    public abstract void onHear(int range);
    
}
