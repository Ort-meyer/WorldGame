using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicTank : MonoBehaviour
{
    public float m_stopDistance;
    public float m_directionMargin;
    public float m_moveSpeed;

    public Transform DEBUG_target;

    NavPathManager m_pathManager;
    //NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {

        m_pathManager = GetComponent<NavPathManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //// Order turret
        GetComponentInChildren<BasicTurret>().M_SetTarget(DEBUG_target);

        
        Vector3 nextCorner = m_pathManager.M_GetNextCorner();
        Vector3 nextToCurrent = nextCorner - transform.position;
        if (nextToCurrent == Vector3.zero)
        {
            return;
        }

        // Debug: just movve towards the next corner and check if we're close enough
        transform.position += nextToCurrent.normalized * m_moveSpeed * Time.deltaTime;

        if(nextToCurrent.magnitude < 0.5f)
        {
            m_pathManager.M_CornerReached();
        }

        // Rotate so we're facing the target

        // If the rotation is enough, move forward

        //float dot = Vector3.Dot(nextToCurrent, transform.forward);
        //if (dot < m_directionMargin && (agent.destination - transform.position).magnitude < m_stopDistance)
        //{
        //    //agent.Move(transform.forward.normalized * m_moveSpeed); evidently moves the agent, not the actual gameobject
        //    transform.position += transform.forward.normalized * m_moveSpeed * Time.deltaTime;
        //}
    }
    
    // This is debuggy. Used directly from camera for debugging purposes. Should have internal management somehow
    public void M_SetDestination(Vector3 destination)
    {
        m_pathManager.M_SetDestination(destination);
    }
}



