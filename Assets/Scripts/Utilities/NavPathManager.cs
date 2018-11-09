using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathManager : MonoBehaviour
{
    private Vector3 m_destination;
    private NavMeshPath m_path;

    private bool m_active = false;
    public float m_pathUpdateFrequency;
    public float m_finalDestinationDistance;

    private int m_nextCornerIndex = 0;
    // Use this for initialization
    void Start()
    {

        m_path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do anything unless we have a destination
        if (!m_active)
        {
            CancelInvoke();
            return;
        }

        InvokeRepeating("UpdatePath", m_pathUpdateFrequency, m_pathUpdateFrequency);
    }

    private void UpdatePath()
    {
        // If the destination is reached, set path to null to make entire component inactive
        if ((transform.position - m_destination).magnitude < m_finalDestinationDistance)
        {
            m_active = false;
        }
        else
        {
            NavMesh.CalculatePath(transform.position, m_destination, NavMesh.AllAreas, m_path);
            m_nextCornerIndex = 0;
        }

    }

    public void M_SetDestination(Vector3 destination)
    {
        m_active = true;
        m_destination = destination;
        UpdatePath();

    }

    public void M_CornerReached()
    {
        m_nextCornerIndex++;
    }

    public void M_ClearDestination()
    {
        m_active = true;
    }

    public Vector3 M_GetNextCorner()
    {
        Vector3 nextCorner = transform.position;
        if (!m_active) // Just to make sure we have a path at all
        {
            return nextCorner;
        }
        // If we haven't reached the end. Should need this but just to make sure
        if (m_nextCornerIndex + 1 < m_path.corners.Length)
        {
            nextCorner = m_path.corners[m_nextCornerIndex + 1];
        }

        return nextCorner;
    }

    public float M_GetDistanceToDestination()
    {
        float distance = 0;
        if(m_path.corners.Length > 0)
        {
            distance = (m_path.corners[m_path.corners.Length-1] - transform.position).magnitude;
        }
        return distance;
    }
}
