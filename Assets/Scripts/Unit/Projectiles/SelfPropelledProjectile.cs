using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPropelledProjectile : BaseProjectile
{
    public AnimationCurve m_accelerationCurve;
    public float m_totalAccelerationDuration;
    private float m_currentAccelerationDuration = 0;
    public float m_maxSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_currentAccelerationDuration += Time.deltaTime;
        transform.position += GetSpeedThisFrame() * transform.forward * Time.deltaTime;
    }

    private float GetSpeedThisFrame()
    {
        float progress = m_currentAccelerationDuration / m_totalAccelerationDuration;
        if (progress > 1)
        {
            progress = 1;
        }
        float speed = m_maxSpeed * m_accelerationCurve.Evaluate(progress);
        return speed;
    }

    public override void M_ProjectileFired(GameObject firingWeapon)
    {
        base.M_ProjectileFired(firingWeapon);
    }
}
