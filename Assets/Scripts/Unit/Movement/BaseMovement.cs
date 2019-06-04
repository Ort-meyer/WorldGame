using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public GameObject m_unit;
    // Use this for initialization
    public virtual void Start()
    {
        m_unit = GetComponentInParent<BaseUnit>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Instructs the unit to move to the specified destination
    /// </summary>
    /// <param name="destination">position of the final destination for the unit</param>
    public virtual void M_MoveOrder(Vector3 destination)
    {

    }

    /// <summary>
    /// Instructs the unit to clear all move orders
    /// </summary>
    public virtual void M_StopOrder()
    {

    }
}
