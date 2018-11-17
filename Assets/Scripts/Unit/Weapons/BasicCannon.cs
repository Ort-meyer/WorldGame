using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCannon : BaseTraverseWeapon
{
    // The margin within the elevation has to be to fire (in degrees)
    public float m_elevationAccuracy;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Fire
        if (m_canFire &&
            Mathf.Abs(m_currentElevation - m_targetElevation) < m_elevationAccuracy)
        {
            FireWeapon();
        }
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        GameObject newBullet = Instantiate(m_projectilePrefab);
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * m_exitVelocity;
        Collider[] ownUnitColliders = m_ownUnit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownUnitColliders)
        {
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), collider);
        }
    }



}
