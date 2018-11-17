using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtgmProjectile : BaseProjectile
{

    public Transform m_target;
    public float m_acceleration; // Should be animation curve
    public float m_maxSpeed;

    private float m_currentSpeed = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   // If target doesn't exist anymore, destroy projectile (this should be done way cooler. Unguided? Spiral out of control?)
        if (m_target == null)
        {
            Destroy(this.gameObject);
            return; // Does this ever get called?
        }

        // Update speed
        m_currentSpeed += m_acceleration * Time.deltaTime;
        if(m_currentSpeed > m_maxSpeed)
        {
            m_currentSpeed = m_maxSpeed;
        }

        // Follow target
        Vector3 toTarget = m_target.position - transform.position;
        transform.position += toTarget.normalized * m_currentSpeed * Time.deltaTime;
        transform.forward = toTarget.normalized;
       
    }

    public override void M_ProjectileFired(GameObject firingWeapon)
    {
        base.M_ProjectileFired(firingWeapon);
        m_target = m_firingWeapon.GetComponent<BaseWeapon>().M_GetTarget();
    }
}
