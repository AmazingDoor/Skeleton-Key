using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Doge : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    GameObject dirt_pile;
    bool digging;
    bool diggin_started;
    bool stand_location_set;
    bool wander_started;
    bool alive = true;
    int health = 1;
    GameObject player;
    NoiseHandler handler;

    int range = 5;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        handler = player.GetComponent<NoiseHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(health <= 0)
        {
            Kill();
        }

        if (alive)
        {
            if (dirt_pile != null)
            {
                handleDirtPile();
            }
            else
            {
                followPlayer();
            }
        }
        
    }

    public void hurt()
    {
        health--;
    }

    void Kill()
    {
        alive = false;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(removeObject());
    }

    IEnumerator removeObject()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }

    void followPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > range)
        {
            agent.SetDestination(player.transform.position);
            stand_location_set = false;
        } else
        {
            if(!stand_location_set)
            {
                setStandLocation();
            }
        }
    }

    void setStandLocation()
    {
        Vector3 newPos = new Vector3(player.transform.position.x + Random.Range(-range, range), player.transform.position.y, player.transform.position.z + Random.Range(-range, range));
        agent.SetDestination(newPos);
        stand_location_set = true;
    }

    IEnumerator Wander()
    {
        wander_started = true;
        Debug.Log("started");
        yield return new WaitForSeconds(Random.Range(10, 20));
        int rand = Random.Range(0, 2);
        if(rand == 1 && !digging)
        {
            stand_location_set = false;
        }
        wander_started = false;
        
    }

    void handleDirtPile()
    {
        if (Vector3.Distance(transform.position, dirt_pile.transform.position) < 2)
        {
            if (digging)
            {
                if (!diggin_started)
                {
                    Dig(dirt_pile);
                }
            }
            agent.SetDestination(transform.position);
        }

    }

    public void setMoveGoal(GameObject goal)
    {
        agent.SetDestination(goal.transform.position);
        dirt_pile = goal;
        digging = true;
    }

    public void Dig(GameObject dirt_pile)
    {
        diggin_started = true;
        StartCoroutine(findObject(dirt_pile));
    }

    IEnumerator findObject(GameObject dirt_pile)
    {
        Debug.Log(dirt_pile.name);
        yield return new WaitForSeconds(4);
        if(dirt_pile.GetComponent<DirtPile>().item)
        {
            GameObject item = dirt_pile.GetComponent<DirtPile>().item;
            Instantiate(item, dirt_pile.transform.position, dirt_pile.transform.rotation);
            Destroy(dirt_pile);
            digging = false;
            diggin_started = false;
            handler.createSound(20, false);
        }

    }
}
