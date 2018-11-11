using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTraverseWeapon : BaseWeapon
{
    // Elevation
    public float m_maxElevation;
    public float m_minElevation;
    public float m_elevationSpeed;
    protected float m_currentElevation = 0;
    protected float m_targetElevation = 0;

    // Traverse (not yet implemented)
    public float m_maxTraverse;
    public float m_traverseSpeed;

    // Fire variables
    public float m_exitVelocity;
    public GameObject m_projectilePrefab;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (m_target == null)
        {
            return;
        }

        UpdateElevation();
    }
    private void UpdateElevation()
    {
        m_targetElevation = 0;

        Vector3 vectorToTarget = (m_target.transform.position - transform.position);
        {
            m_targetElevation = GetAngle(vectorToTarget.magnitude, vectorToTarget.y) * Mathf.Rad2Deg * -1;
        }

        // Calculate target angle
        // Vector between barrel origin and target

        //float targetElevation = Mathf.Atan((4.91f * vectorToTarget.magnitude) / m_exitVelocity) * Mathf.Rad2Deg * -1;

        float diffAngle = m_targetElevation - m_currentElevation;

        if (Mathf.Abs(diffAngle) > 0)
        {
            float rotateAngle = Mathf.Sign(diffAngle) * m_elevationSpeed * Time.deltaTime;

            // If we overshoot, set rotate to diff for perfect rotate
            if (Mathf.Abs(rotateAngle) > Mathf.Abs(diffAngle))
            {
                rotateAngle = diffAngle;
            }
            transform.Rotate(rotateAngle, 0, 0, Space.Self);
            m_currentElevation += rotateAngle;
        }
    }

    // Taken from https://www.gamedev.net/forums/topic/107074-calculating-projectile-launch-angle-to-hit-a-target/?page=3
    // Returns angle necessary to hit the target with the given parameters
    private float GetAngle(float distanceToTarget, float heightDifference)
    {
        // Broken up for debugging purposes. Keeping it around for readability
        float u = m_exitVelocity;
        float us = Mathf.Pow(u, 2);
        float x = distanceToTarget;
        float xs = Mathf.Pow(x, 2);
        float y = heightDifference;
        float g = 9.81f;

        float xsus = xs / us;
        float gxsus = g * xsus;
        float part0 = y + 0.5f * gxsus;
        float part1 = u - Mathf.Sqrt((us - 2 * g * part0));
        float part2 = g * (x / u);
        float angle = Mathf.Atan(part1 / part2);

        return angle;
    }
}
