using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : BaseWeapon
{
    public GameObject m_projectilePrefab;
    public float m_exitVelocity;

    private TraverseWeapon m_traverseWeaponScript;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        m_traverseWeaponScript = GetComponent<TraverseWeapon>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (m_target != null)
        {
            Vector3 vectorToTarget = (m_target.transform.position - transform.position);
            float targetTraverse = Helpers.GetDiffAngle2D(transform.forward, vectorToTarget);
            float targetElevation = GetAngle(vectorToTarget.magnitude, vectorToTarget.y) * Mathf.Rad2Deg * -1;
            m_traverseWeaponScript.M_SetTargetAngles(targetTraverse, targetElevation);
            if(m_canFire)
            {
                FireWeapon();
            }
        }
        // If we don't have a target, reset the barrel
        else
        {
            m_traverseWeaponScript.M_SetTargetAngles(0, 0);
        }
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        GameObject newBullet = Instantiate(m_projectilePrefab);
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * m_exitVelocity;
        foreach (BaseProjectile projectileScript in newBullet.GetComponents<BaseProjectile>())
        {
            projectileScript.M_ProjectileFired(this.gameObject);
        }
        Collider[] ownUnitColliders = m_ownUnit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownUnitColliders)
        {
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), collider);
        }
    }

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
