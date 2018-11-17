using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public enum WeaponType { smallCannon, lightCannon, mediumCanon, LightLauncher, MediumLauncher};
    public WeaponType m_weaponType;
    public float m_fireCooldown;

    protected Transform m_target = null;
    protected float m_currentCooldown = 0.0f;
    protected bool m_canFire = false;

    private bool m_allowFire = false;

    protected GameObject m_ownUnit;

    // Use this for initialization
    protected virtual void Start()
    {
        m_ownUnit = GetComponentInParent<BaseUnit>().gameObject;
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
    public virtual void M_AllowFire()
    {
        m_allowFire = true;
    }

    public virtual void M_HoldFire()
    {
        m_allowFire = false;
    }

    // Sets the target for this weapon
    public virtual void M_SetTarget(Transform target)
    {
        m_target = target;
    }

    // Clears the target
    public void M_ClearTarget()
    {
        m_target = null;
    }

    public Transform M_GetTarget()
    {
        return m_target;
    }
}
