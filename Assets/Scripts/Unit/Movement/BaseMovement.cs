using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

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
