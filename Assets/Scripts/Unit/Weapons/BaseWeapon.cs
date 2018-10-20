using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    public float m_fireCooldown;

    protected Transform m_target = null;
    protected float m_currentCooldown = 0.0f;
    protected bool m_canFire = true;

    private bool m_allowFire = false;

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        m_currentCooldown -= Time.deltaTime;
        if (m_currentCooldown <= 0 && m_allowFire)
        {
            m_canFire = true;
        }
    }

    protected virtual void FireWeapon()
    {
        m_canFire = false;
        m_currentCooldown = m_fireCooldown;
    }

    // Sets whether the weapon should fire or not
    public void M_AllowFire()
    {
        m_allowFire = true;
    }

    public void M_HoldFire()
    {
        m_allowFire = false;
    }

    // Sets the target for this weapon
    public void M_SetTarget(Transform target)
    {
        m_target = target;
    }

    // Clears the target
    public void M_ClearTarget()
    {
        m_target = null;
    }
}
