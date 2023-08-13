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
    GameObject player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

        
        if(dirt_pile != null)
        {
            handleDirtPile();
        } else
        {
            followPlayer();
        }
        
    }

    void followPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            agent.SetDestination(player.transform.position);
        } else
        {
            agent.SetDestination(transform.position);
        }
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
        }

    }
}
