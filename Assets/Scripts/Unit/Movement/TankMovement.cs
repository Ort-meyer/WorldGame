using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : BaseMovement
{
    public float m_moveSpeed;
    public float m_rotateSpeed;
    public float m_directionMargin;

    NavPathManager m_pathManager;

    //private GameObject m_parent;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        m_pathManager = GetComponent<NavPathManager>();
        //m_parent = GetComponentInParent<BaseUnit>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_pathManager.M_DestinationReached())
        {
            // Get next corner
            Vector3 nextCorner = m_pathManager.M_GetNextCorner();
            Vector3 nextToCurrent = nextCorner - transform.position;

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
                m_unit.transform.Rotate(0, rotateAngle, 0, Space.World); // What happens if the tank tilts? Should be Space.World?
            }

            // If the rotation is enough, move forward
            if (Mathf.Abs(angle) < m_directionMargin)
            {
                m_unit.transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
            }

        }
    }


    public override void M_MoveOrder(Vector3 destination)
    {
        m_pathManager.M_SetDestination(destination);
    }

    public override void M_StopOrder()
    {
        m_pathManager.M_ClearDestination();
    }
}
