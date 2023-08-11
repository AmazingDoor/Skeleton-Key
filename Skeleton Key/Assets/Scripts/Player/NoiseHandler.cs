using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NoiseHandler : MonoBehaviour
{

    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, 10);
    }
    // Start is called before the first frame update

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void createSound(int range, bool heardThroughWall)
    {
        
        Collider [] monsters = Physics.OverlapSphere(transform.position, range);
        List<Collider> new_monsters = new List<Collider>() { };
        foreach (Collider m in monsters)
        {
            if (!new_monsters.Contains(m))
            {
                if (m.CompareTag("monster")) { new_monsters.Add(m); }
                if(m.GetComponentInParent<StalkerMove>()) { new_monsters.Remove(m); }
                if(m.gameObject.name == "Head") { new_monsters.Remove(m); }
                if(!m.GetComponentInParent<NavMeshAgent>()) { new_monsters.Remove(m); }
            }
        }


        if (heardThroughWall == false)
        {
            foreach(Collider m in new_monsters)
            {
                RaycastHit hit;
                Physics.Linecast(m.transform.position, transform.position, out hit);
                Debug.DrawLine(m.transform.position, transform.position);
                if (hit.transform.CompareTag("Player"))
                {
                    m.GetComponentInParent<BaseMonster>().onHear(range);
                }

            }
        } else
        {
            foreach (Collider m in new_monsters)
            {
                m.GetComponentInParent<BaseMonster>().onHear(range);
                //m.GetComponentInParent<BaseMonster>().setPositionGoal(transform);
            }
        }
    }
}
