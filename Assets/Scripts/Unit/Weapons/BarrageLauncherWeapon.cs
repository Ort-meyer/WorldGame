using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageLauncherWeapon : BaseWeapon
{

    public Transform[] m_launchPositions;

    private int m_currentLaunchPosition = 0;
    public GameObject m_projectilePrefab;

    public float m_timeBetweenLaunches;
    private float m_currentLaunchTimer = 0;
    private bool m_firing = false;

    private List<Collider> m_missileColliders = new List<Collider>();

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        // Make total cooldown include the barrage time
        m_fireCooldown += m_launchPositions.Length * m_timeBetweenLaunches;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_target == null)
        {
            return;
        }
        base.Update();

        if (m_canFire)
        {
            FireWeapon();
        }

        if (m_firing)
        {
            m_currentLaunchTimer += Time.deltaTime;
            if (m_currentLaunchTimer >= m_timeBetweenLaunches)
            {
                LaunchProjectile();
                m_currentLaunchPosition++;
                // If barrage is over, top firing
                if (m_currentLaunchPosition >= m_launchPositions.Length)
                {
                    m_firing = false;
                    m_currentLaunchPosition = 0;
                }
                m_currentLaunchTimer = 0;
            }
        }
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        m_firing = true;
        m_currentLaunchTimer = 0;
        m_currentLaunchPosition = 0;
    }

    private void LaunchProjectile()
    {
        // Create rocket
        GameObject newRocket = Instantiate(m_projectilePrefab);
        newRocket.transform.position = m_launchPositions[m_currentLaunchPosition].position;
        newRocket.transform.rotation = m_launchPositions[m_currentLaunchPosition].rotation;
        foreach (BaseProjectile projectileScript in newRocket.GetComponents<BaseProjectile>())
        {
            projectileScript.M_ProjectileFired(this.gameObject);
        }

        Collider thisRocketCollider = newRocket.GetComponent<Collider>();
        Collider[] ownUnitColliders = m_ownUnit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownUnitColliders)
        {
            Physics.IgnoreCollision(thisRocketCollider, collider);
        }
    }
}
