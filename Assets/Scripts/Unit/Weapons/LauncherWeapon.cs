using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherWeapon : BaseWeapon
{

    public Transform[] m_launchPositions;
    private int m_currentLaunchPosition = 0;

    public GameObject m_projectilePrefab;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (m_canFire)
        {
            FireWeapon();
        }
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        LaunchProjectile();
        m_currentLaunchPosition++;
        if(m_currentLaunchPosition >= m_launchPositions.Length)
        {
            m_currentLaunchPosition = 0;
        }
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
