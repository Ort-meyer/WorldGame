using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathManager : MonoBehaviour
{
    private Vector3 m_destination;
    private NavMeshPath m_path;
    

    public float m_pathUpdateFrequency;

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
        if(m_destination == null || m_path == null)
        {
            CancelInvoke();
            return;
        }

        InvokeRepeating("UpdatePath", m_pathUpdateFrequency, m_pathUpdateFrequency);
    }

    private void UpdatePath()
    {
        NavMesh.CalculatePath(transform.position, m_destination, NavMesh.AllAreas, m_path);
        m_nextCornerIndex = 0;
    }

    public void M_SetDestination(Vector3 destination)
    {
        m_destination = destination;
        UpdatePath();
    }

    public void M_CornerReached()
    {
        m_nextCornerIndex++;
    }

    public void M_ClearDestination()
    {
        m_path = null;
    }

    public Vector3 M_GetNextCorner()
    {
        // If we're at the end of the path, just return where we're standing right now
        Vector3 nextCorner = transform.position;
        if(m_nextCornerIndex+1 < m_path.corners.Length)
        {
            nextCorner = m_path.corners[m_nextCornerIndex+1];
        }
        return nextCorner;
    }
}
