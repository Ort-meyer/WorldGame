using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    // The target that the turret should be facing
    protected Transform m_target = null;
    // The list of all targets that the turret is set to engage
    public List<GameObject> m_targets;
    // Top owner of the turret (the unit)
    protected GameObject m_owner;

    // Use this for initialization
    protected virtual void Start()
    {
        m_owner = GetComponentInParent<BaseUnit>().gameObject;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(m_target == null)
        {
            return;
        }
    }

    virtual public void M_SetTargets(List<GameObject> targets)
    {
        m_targets = targets;
    }

    virtual public void M_ClearTarget()
    {
        m_target = null;
        m_targets.Clear();
    }
}

