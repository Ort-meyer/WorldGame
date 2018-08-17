using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicTank : MonoBehaviour
{
    public float m_moveSpeed;
    public float m_rotateSpeed;
    public float m_directionMargin;
    public float m_cornerIncrementDistance;

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

        if (nextToCurrent.magnitude < m_cornerIncrementDistance)
        {
            m_pathManager.M_CornerReached();
        }

        // Rotate so we're facing the target
        float angle = Helpers.GetDiffAngle2D(transform.forward, nextToCurrent);
        // If we're not looing at the target, turn the turret
        if (Mathf.Abs(angle) > 0)
        {
            float rotateAngle = Mathf.Sign(angle) * m_rotateSpeed * Time.deltaTime;

            // If we overshoot, set rotate to diff for perfect rotate
            if (Mathf.Abs(rotateAngle) > Mathf.Abs(angle))
            {
                rotateAngle = angle;
            }
            transform.Rotate(0, rotateAngle, 0, Space.World); // What happens if the tank tilts? Should be Space.World?
        }

        // If the rotation is enough, move forward
        if(Mathf.Abs(angle) < m_directionMargin)
        {
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }

    }
    
    // This is debuggy. Used directly from camera for debugging purposes. Should have internal management somehow
    public void M_SetDestination(Vector3 destination)
    {
        m_pathManager.M_SetDestination(destination);
    }
}