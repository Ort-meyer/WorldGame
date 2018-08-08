using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{

    private Transform m_target;
    public GameObject m_ownTank;

    // Fire variables
    public float m_exitVelocity;
    public GameObject m_projectilePrefab;
    public float m_fireCooldown;
    private float m_currentFireCooldown;
    private bool m_canFire;

    // Elevation
    public float m_maxElevation;
    public float m_minElevation;
    public float m_elevationSpeed;
    private float m_currentElevation = 0;

    // Traverse (not yet implemented)
    public float m_maxTraverse;
    public float m_traverseSpeed;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //// First we just point the gun. Then we try to elevate it to account for drop
        //// Vector between barrel origin and target
        //Vector3 vectorToTarget = (transform.position - m_target.transform.position).normalized;
        //// Current direction that barrel points
        //Vector3 forward = transform.forward.normalized;
        //// Rotate to point in the direction of the target
        //Vector3 currentTheoreticalVectorToTarget = new Vector3(vectorToTarget.x, forward.y, vectorToTarget.z).normalized;

        if(m_target == null)
        {
            return;
        }

        float targetElevation = 0;

        Vector3 vectorToTarget = (m_target.transform.position - transform.position);
        {
            targetElevation = GetAngle(vectorToTarget.magnitude, vectorToTarget.y) * Mathf.Rad2Deg * -1;
        }

        // Calculate target angle
        // Vector between barrel origin and target
        
        //float targetElevation = Mathf.Atan((4.91f * vectorToTarget.magnitude) / m_exitVelocity) * Mathf.Rad2Deg * -1;

        float diffAngle = targetElevation - m_currentElevation;

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


        // Fire
        if (m_canFire)
        {
            m_canFire = false;
            m_currentFireCooldown = m_fireCooldown;
            GameObject newBullet = Instantiate(m_projectilePrefab);
            newBullet.transform.rotation = transform.rotation;
            newBullet.transform.position = transform.position;
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * m_exitVelocity;
            Collider[] ownTankColliders = m_ownTank.GetComponentsInChildren<Collider>();
            foreach (Collider collider in ownTankColliders)
            {
                Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), collider);
            }
        }
        else
        {
            m_currentFireCooldown -= Time.deltaTime;
            if(m_currentFireCooldown <= 0)
            {
                m_canFire = true;
            }
        }
    }

    public void M_SetTarget(Transform target)
    {
        m_target = target;
    }

    public void M_Cleararget()
    {
        m_target = null;
    }

    // Taken from https://en.wikipedia.org/wiki/Range_of_a_projectile
    // WE DON'T NEED THIS but I keep it around. It works wonders
    private float GetDistanceAtAngle(float angle, float heightDifference)
    {
        float v = m_exitVelocity;
        float g = 9.81f;
        float ys = heightDifference;
        float a = angle;
        float distance = (Mathf.Pow(v, 2) / (2 * g)) * (1 + Mathf.Sqrt(1 + (2 * g * ys) / (Mathf.Pow(v, 2) * Mathf.Pow(Mathf.Sin(a), 2)))) * Mathf.Sin(2 * a);
        return distance;
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
