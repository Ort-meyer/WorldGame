using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTurret : BaseTurret
{
    public float m_rotationSpeed;

    private float m_targetAngle = 0;
    private float m_currentAngle = 0;

    private GameObject m_owner;

    // Use this for initialization
    protected override void Start()
    {
        m_owner = GetComponentInParent<BaseUnit>().gameObject;
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Rotate towards target
        float diffAngle = 0;
        // Rotate towards target if it is set
        if (m_target)
        {
            diffAngle = Helpers.GetDiffAngle2D(transform.forward, m_target.position - transform.position);
        }
        // Rotate towards owning tank's forward if not (should probably have some default-forward for all turrets instead)
        else
        {
            diffAngle = Helpers.GetDiffAngle2D(transform.forward, m_owner.transform.forward);
        }

        if (Mathf.Abs(diffAngle) > 0) // Add epsilon, or better yet: if we overshoot, set rotateAngle to diffAngle
        {
            float rotateAngle = Mathf.Sign(diffAngle) * m_rotationSpeed * Time.deltaTime;

            // If we overshoot, set rotate to diff for perfect rotate
            if (Mathf.Abs(rotateAngle) > Mathf.Abs(diffAngle))
            {
                rotateAngle = diffAngle;
            }
            transform.Rotate(0, rotateAngle, 0, Space.World); // What happens if the tank tilts? Should be Space.World?
        }

        foreach (BaseWeapon weapon in GetComponentsInChildren<BaseWeapon>())
        {
            if (Mathf.Abs(diffAngle) < 10)
            {
                weapon.M_SetTarget(m_target);
                weapon.M_AllowFire();
            }
            else
            {
                weapon.M_ClearTarget();
                weapon.M_HoldFire();
            }
        }
    }
}
