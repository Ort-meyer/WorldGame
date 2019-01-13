using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public int m_alignment;
    public float m_hp;
    public Unit m_unit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Instructs the unit to move to the specified position
    /// </summary>
    /// <param name="position">Target destination to move to</param>
    public virtual void M_MoveOrder(Vector3 destination)
    {

    }
    
    /// <summary>
    /// Instructs the unit to attack the target gameobject
    /// </summary>
    /// <param name="target">GameObject which should be attacked</param>
    public virtual void M_AttackOrder(List<GameObject> target)
    {

    }
    /// <summary>
    /// Instructs the unit to clear all orders
    /// </summary>
    public virtual void M_StopOrder()
    {

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
