using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaiderCommander : MonoBehaviour
{
    NavMeshAgent agent;
    // Use this for initialization
    public Transform m_target;

    // Distances: chaseDistance > engagementDistance > disengageDistance
    // The optimum distance at which the tank wants to engage
    public float m_engagementDistance;
    // If the target is within this distance, the tank moves away
    public float m_disengageDistance;
    // If the target is further away than this distance, the tank moves to engagement distance
    public float m_chaseDistance;
    // The distance within which the tank will stop when moving to an engagement point
    public float m_engagementPointMargin;

    [SerializeField]
    bool inEngagementZone = false;

    // Use this for initialization
    void Start()
    {
        //GetComponentInChildren<BaseTurret>().M_SetTarget(m_target);

        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetToTank = transform.position - m_target.transform.position;
        float currentDistance = targetToTank.magnitude;

        if(!inEngagementZone)
        {
            // The position of engagement
            Vector3 engagementPoint = m_target.position + targetToTank.normalized * m_engagementDistance;
            MoveToPosition(engagementPoint);

            // See if we're back in the engagement zone
            float distanceToEngagementPoint = (engagementPoint - transform.position).magnitude;
            if(currentDistance < distanceToEngagementPoint)
            {
                inEngagementZone = true;
            }
        }

        // Check engagement zone
        if(currentDistance < m_disengageDistance ||
            currentDistance > m_chaseDistance)
        {
            inEngagementZone = false;
        }
    }

    private void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
    }


}
