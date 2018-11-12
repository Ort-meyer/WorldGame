using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : BaseTurret
{

    public float m_rotationAccuracy;

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
        // Let all weapons on this turret know that they can fire
        bool allTurretsFire = Mathf.Abs(m_diffAngle) < m_rotationAccuracy;
        {
            foreach(BaseWeapon weapon in GetComponentsInChildren<BaseWeapon>())
            {
                if(allTurretsFire && m_target != null)
                {
                    weapon.M_AllowFire();
                }
                else
                {
                    weapon.M_HoldFire();
                }
            }
        }
    }

    public override void M_SetTarget(Transform target)
    {
        base.M_SetTarget(target);
        foreach(BaseWeapon weapon in GetComponentsInChildren<BaseWeapon>())
        {
            weapon.M_SetTarget(target);
        }
    }
}
