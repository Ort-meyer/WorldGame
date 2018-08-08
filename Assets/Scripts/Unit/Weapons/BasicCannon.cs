using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCannon : BaseBallisticWeapon
{


    // Traverse (not yet implemented)
    public float m_maxTraverse;
    public float m_traverseSpeed;

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
        if (m_canFire)
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
        Collider[] ownTankColliders = m_ownTank.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownTankColliders)
        {
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), collider);
        }
    }



}
