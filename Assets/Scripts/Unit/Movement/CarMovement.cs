using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : BaseMovement
{

    public float m_forwardForce;
    public float m_friction;

    public float m_turnDistance;

    public float m_breakForce;

    // The speed at which the car just stops
    public float m_fullStopSpeed;

    // The maximum angle of the wheels
    public float m_maxWheelAngle = 45;
    // How far forward the wheels are relative to centre of car (this can be done better via transforms in editor, for instance)
    public float m_wheelDistance = 1;

    private float m_wheelAngle = 0;

    private NavPathManager m_pathManager;
    private Rigidbody m_rigidbody;
    // Use this for initialization
    void Start()
    {
        m_pathManager = GetComponent<NavPathManager>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private float DEBUGTimer = 0;

    // Update is called once per frame
    void Update()
    {
        // Get next corner
        Vector3 nextCorner = m_pathManager.M_GetNextCorner();
        if (nextCorner == transform.position)
        {
            return;
        }

        Vector3 nextToCurrent = nextCorner - transform.position;
        // If distance is short enough, move to next corner
        //if (nextToCurrent.magnitude < m_turnDistance)
        //{
        //    m_pathManager.M_CornerReached();
        //}
        // Just test to drive forward for a few seconds, then break;
        DEBUGTimer += Time.deltaTime;
        if (DEBUGTimer < 3)
        {
            DriveForward(nextToCurrent);
        }
        else
        {
            Break();
            MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);
        }
    }

    private void DriveForward(Vector3 direction)
    {

        Quaternion forceRotation = transform.rotation;
        Vector3 forceForward = forceRotation * transform.forward;
        Vector3 forcePosition = transform.position + transform.forward.normalized * m_wheelDistance;

        // Now apply force
        Vector3 force = forceForward * m_forwardForce * Time.deltaTime;
        Helpers.DrawDebugLine(forcePosition, forcePosition + force);
        m_rigidbody.AddForceAtPosition(forceForward * m_forwardForce * Time.deltaTime, forcePosition);

        //float angle = Helpers.GetDiffAngle2D(transform.forward, direction);
        //// Limit wheel angle (wheels are instant atm)
        //if (Mathf.Abs(angle) > m_maxWheelAngle)
        //{
        //    angle = Mathf.Sign(angle) * m_maxWheelAngle;
        //}

        //// The transform that will be used to apply force to the car
        //Quaternion forceRotation = transform.rotation * Quaternion.Euler(transform.up * angle);
        //Vector3 forceForward = forceRotation * transform.forward;
        //Vector3 forcePosition = transform.position + transform.forward.normalized * m_wheelDistance;

        //// Now apply force
        //Vector3 force = forceForward * m_forwardForce * Time.deltaTime;
        //Helpers.DrawDebugLine(forcePosition, forcePosition + force);
        //m_rigidbody.AddForceAtPosition(forceForward * m_forwardForce * Time.deltaTime, forcePosition);
    }

    private void Break()
    {
        // Break is that it just stops, for now
        if (m_rigidbody.velocity.magnitude < m_fullStopSpeed)
        {
            m_rigidbody.velocity = new Vector3();
        }
        else
        {
            m_rigidbody.AddForce(-1 * transform.forward.normalized * m_breakForce * Time.deltaTime);
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
