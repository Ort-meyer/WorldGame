using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicTank : BaseUnit
{



    //NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public override void M_MoveOrder(Vector3 destination)
    {
        BaseMovement movement = GetComponentInChildren<BaseMovement>();
        if (movement)
        {
            movement.M_MoveOrder(destination);
        }
    }

    public override void M_StopOrder()
    {
        BaseMovement movement = GetComponent<BaseMovement>();
        if (movement)
        {
            movement.M_StopOrder();
        }
    }

    public override void M_AttackOrder(List<GameObject> targets)
    {
        // Pass along targeting order to targeting component, if we have one
        BaseTargeting targeting = GetComponent<BaseTargeting>();
        if(targeting)
        {
            targeting.M_SetTargets(targets);
        }
    }


}