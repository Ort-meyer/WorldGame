using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtgmLauncher : BaseWeapon
{


    private enum LauncherState { LockingOn, LockedOn, CeaseFire };

    public GameObject m_projectilePrefab;
    public float m_lockonTime;

    public Transform[] m_launchPositions;

    private LauncherState m_launcherState = LauncherState.CeaseFire;
    private int m_currentLaunchPosition = 0;

    // Use this for initialization
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (m_launcherState == LauncherState.LockingOn)
        {
            m_lockonTime -= Time.deltaTime;
            if (m_lockonTime <= 0)
            {
                m_launcherState = LauncherState.LockedOn;
            }
        }

        if (m_launcherState == LauncherState.LockedOn && m_canFire) // Has to be both locked on and off cooldown
        {
            FireWeapon();
        }
    }

    public override void M_AllowFire()
    {
        base.M_AllowFire();
        m_launcherState = LauncherState.LockingOn;
    }

    public override void M_HoldFire()
    {
        base.M_HoldFire();
    }

    public override void M_SetTarget(Transform target)
    {
        base.M_SetTarget(target);
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        m_launcherState = LauncherState.LockingOn;
        // Create rocket
        GameObject newRocket = Instantiate(m_projectilePrefab);
        newRocket.transform.position = m_launchPositions[m_currentLaunchPosition].position;
        newRocket.transform.rotation = m_launchPositions[m_currentLaunchPosition].rotation;
        foreach (BaseProjectile projectileScript in newRocket.GetComponents<BaseProjectile>())
        {
            projectileScript.M_ProjectileFired(this.gameObject);
        }

        // Todo: move to specific projectile
        Collider[] ownTankColliders = m_ownTank.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownTankColliders)
        {
            Physics.IgnoreCollision(newRocket.GetComponent<Collider>(), collider);
        }

        // Change to next launcher position
        m_currentLaunchPosition++;
        if (m_currentLaunchPosition >= m_launchPositions.Length)
        {
            m_currentLaunchPosition = 0;
        }
    }
}
