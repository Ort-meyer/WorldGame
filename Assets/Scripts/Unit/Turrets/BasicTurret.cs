using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{



    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_currentRotationSpeed = m_maxRotationSpeed;
    }

    public override void M_SetTarget(Transform target)
    {
        base.M_SetTarget(target);
        GetComponentInChildren<BasicCannon>().M_SetTarget(target);
    }
}
