using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTargeting : BaseTargeting
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Set all turrets to engage closest target each frame
        GameObject target = null;
        float closest = 10000;
        foreach (GameObject obj in m_targets)
        {
            float distanceToTarget = (obj.transform.position - transform.position).magnitude;
            if(distanceToTarget < closest)
            {
                closest = distanceToTarget;
                target = obj;
            }
        }

        if (target) // Ensure that the object still exist
        {
            foreach (BaseTurret turret in GetComponentsInChildren<BaseTurret>())
            {
                turret.M_SetTarget(target.transform);
            }
        }
    }

    public override void M_SetTargets(List<GameObject> targets)
    {
        base.M_SetTargets(targets);
    }
}
