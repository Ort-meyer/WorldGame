using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicTank : BaseUnit
{



    //NavMeshAgent agent;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void M_MoveOrder(Vector3 destination)
    {
        base.M_MoveOrder(destination);
    }

    public override void M_StopOrder()
    {
        base.M_StopOrder();
    }

    public override void M_AttackOrder(List<GameObject> targets)
    {
        base.M_AttackOrder(targets);
        //// Pass along targeting order to targeting component, if we have one
        //BaseTargeting targeting = GetComponent<BaseTargeting>();
        //if(targeting)
        //{
        //    targeting.M_SetTargets(targets);
        //}
    }


}