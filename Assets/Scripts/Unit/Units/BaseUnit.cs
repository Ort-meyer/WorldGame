using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    // Which player this unit belongs to
    public int m_alignment;
    // How much HP this unit has. When it'z zero, it is destroyed
    public float m_hp;
    // Movement component
    public BaseMovement m_movement;
    // Transform which the unit will continually move towards
    public Transform m_followTarget;
    // Distance to follow target which the unit will try to reach before stopping
    public float m_followDistance;

    // Use this for initialization
    public virtual void Start()
    {
        m_movement = GetComponentInChildren<BaseMovement>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Moves towards a transform each frame (follow)
        if (m_followTarget)
        {
            float distanceToTarget = Vector3.Magnitude(m_followTarget.transform.position - transform.position);
            if(distanceToTarget >= m_followDistance)
            {
                M_MoveOrder(m_followTarget.transform.position);
            }
            else
            {
                M_StopOrder();
            }
        }
    }

    /// <summary>
    /// Instructs the unit to move to the specified position
    /// </summary>
    /// <param name="position">Target destination to move to</param>
    public virtual void M_MoveOrder(Vector3 destination)
    {
        if (m_movement)
        {
            m_movement.M_MoveOrder(destination);
        }
    }
    
    /// <summary>
    /// Instructs the unit to attack the target gameobject
    /// </summary>
    /// <param name="target">GameObject which should be attacked</param>
    public virtual void M_AttackOrder(List<GameObject> targets)
    {
        // Start following closest target
        GameObject closestTarget = Helpers.FindClosestObject(gameObject, targets);
        m_followTarget = closestTarget.transform;
        // Set all turrets to engage all targets
        foreach(BaseTurret turret in GetComponentsInChildren<BaseTurret>())
        {
            turret.M_SetTargets(targets);
        }
    }
    /// <summary>
    /// Instructs the unit to clear all orders
    /// </summary>
    public virtual void M_StopOrder()
    {
        if (m_movement)
        {
            m_movement.M_StopOrder();
        }
    }

    public virtual void M_InflictDamage(float damage)
    {
        m_hp -=damage;
        if(m_hp <= 0)
        {
            Destroy(this.gameObject);   
        }
    }
}
