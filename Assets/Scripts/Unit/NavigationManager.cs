using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour {
    public float m_stopDistance;


    public Transform DEBUG_target;
    NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if((agent.destination - transform.position).magnitude < m_stopDistance)
        {
            M_Stop();
        }
        // Order turret
        GetComponentInChildren<BasicTurret>().M_SetTarget(DEBUG_target);
    }

    public void M_SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
        M_Resume();
    }

    public void M_Stop()
    {
        agent.isStopped = true;
    }

    public void M_Resume()
    {
        agent.isStopped = false;
    }
}

