using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : BaseMovement
{
    public float m_maxMoveSpeed;
    public float m_acceleration;
    public float m_turnTime;

    public GameObject[] frontWheels;
    public GameObject[] rearWheels;

    // The maximum angle of the wheels
    public float m_maxWheelAngle = 45;

    private NavPathManager m_pathManager;

    private GameObject m_parent;

    private float m_currentMoveSpeed = 0;
    // Use this for initialization
    void Start()
    {
        m_pathManager = GetComponent<NavPathManager>();
        m_parent = GetComponentInParent<BaseUnit>().gameObject;
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
        if (true || DEBUGTimer < 3)
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
        float angle = Helpers.GetDiffAngle2D(transform.forward, direction);
        // Limit wheel angle (wheels are instant atm)
        if (Mathf.Abs(angle) > m_maxWheelAngle)
        {
            angle = Mathf.Sign(angle) * m_maxWheelAngle;
        }

        // Face the wheels correctly
        Vector3 wheelForward = new Vector3(0,0,0);
        foreach(GameObject obj in frontWheels)
        {
            obj.transform.localRotation = Quaternion.Euler(0, angle, 0);
            wheelForward = obj.transform.forward;
        }

        // Move car forward
        Acceleration();
        Vector3 movement = transform.forward * m_currentMoveSpeed * Time.deltaTime;
        m_parent.transform.position += movement;

        // Rotate car depending on wheel angle
        Vector3 pivotPoint = transform.position; // Should be rear wheels, I reckon
        m_parent.transform.RotateAround(pivotPoint, m_parent.transform.up, angle * m_turnTime * Time.deltaTime);

    }

    private void Acceleration()
    {
        // Todo: accelerate according to animation curve (quick in start, slow at end)
        m_currentMoveSpeed += m_acceleration * Time.deltaTime;
        if (m_currentMoveSpeed > m_maxMoveSpeed)
        {
            m_currentMoveSpeed = m_maxMoveSpeed;
        }
    }

    private void Break()
    {
        // Break is that it just stops, for now
        //if (m_rigidbody.velocity.magnitude < m_fullStopSpeed)
        //{
        //    m_rigidbody.velocity = new Vector3();
        //}
        //else
        //{
        //    m_rigidbody.AddForce(-1 * transform.forward.normalized * m_breakForce * Time.deltaTime);
        //}
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
